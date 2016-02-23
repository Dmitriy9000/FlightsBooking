using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NLog;
using PhaseTicket.DeviceCommanderRef;
using PhaseTicket.Models;
using PhaseTicket.Models.XML.CreateOrder;
using PhaseTicket.Models.XML.GetOrderInfo;

namespace PhaseTicket.Controllers
{
    public class TerminalController : Controller
    {
        private readonly AwadAPI _api;

        public TerminalController(AwadAPI api)
        {
            _api = api;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Devices()
        {
            return View();
        }

        public ActionResult Devices2()
        {
            return View("DevicesIsolatedScenario", "_LayoutDevices");
        }

        [HttpPost]
        public ActionResult InitRequest(string from, string to, DateTime? d1, DateTime? d2, ServiceClass cl, 
            int? amateurs, int? childs, int? infants )
        {
            if (amateurs.HasValue || childs.HasValue)
            {
                var travelPoints = new List<TravelEpisode>();

                if (d1.HasValue)
                {
                    travelPoints.Add(new TravelEpisode()
                    {
                        DeparturePoint = from,
                        DepartureDate = d1.Value,
                        ArrivePoint = to
                    });
                }
                if (d2.HasValue)
                {
                    travelPoints.Add(new TravelEpisode()
                    {
                        DeparturePoint = to,
                        DepartureDate = d2.Value,
                        ArrivePoint = from
                    });
                }

                try
                {
                    var result = _api.InitRequest(travelPoints, amateurs.Value, childs ?? 0, infants?? 0, cl);
                    return Success(result);
                }
                catch (AwadException e)
                {
                    return Error(e.Message);
                }
            }

            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult SearchStatus(string r)
        {
            try
            {
                var result = _api.SearchStatus(r);
                return Success(result);
            }
            catch (AwadException e)
            {
                return Error(e.Message);
            }
        }

        [HttpPost]
        public ActionResult SearchResult(string r)
        {
            try
            {
                var t = new Stopwatch();
                t.Start();

                var culture = new CultureInfo("ru-RU");

                var result = _api.Fares(r);
                var e1 = t.Elapsed;

                int did = 0;
                
                // Additional info (start and end time and date, flight number, time in air)
                foreach (var airline in result.Airlines)
                {
                    foreach (var fare in airline.Fare)
                    {
                        fare.Additional = _api.FareDetails(r, fare.Id);
                        fare.Additional.Airline = airline.Name;

                        //fare.Price = AmountModel.CalculateAmount(decimal.Parse(fare.Price));
                        fare.PriceWithComission = AmountModel.CalculateAmount(decimal.Parse(fare.Price));
                        fare.Comission = AmountModel.CalculateComission(decimal.Parse(fare.Price));
                        foreach (var dir in fare.Additional.Dirs)
                        {
                            dir.Id = did.ToString();
                            did++;

                            foreach (var fareDetailsVariant in dir.Variants)
                            {
                                fareDetailsVariant.Legs[0].Departure.Date =
                                    DateTime.Parse(fareDetailsVariant.Legs[0].Departure.Date).ToShortDateString();
                                fareDetailsVariant.Legs[0].Arrival.Date =
                                    DateTime.Parse(fareDetailsVariant.Legs[0].Arrival.Date).ToShortDateString();

                                var day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fareDetailsVariant.Legs[0].Departure.DayOfWeek);
                                fareDetailsVariant.Legs[0].Departure.DayOfWeek = culture.DateTimeFormat.GetDayName(day);

                                day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), fareDetailsVariant.Legs[0].Arrival.DayOfWeek);
                                fareDetailsVariant.Legs[0].Arrival.DayOfWeek = culture.DateTimeFormat.GetDayName(day);
                            }
                        }

                    }
                }

                var totalFares = result.Airlines.SelectMany(c => c.Fare).OrderBy(c=>c.PriceWithComission);

                var e2 = t.Elapsed;
                return Success(totalFares);
            }
            catch (AwadException e)
            {
                return Error(e.Message);
            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        [HttpPost]
        public ActionResult Rules(string r, string f)
        {
            try
            {
                var result = _api.FareRules(r, f);
                var j = Json(result);
                return Success(j);
            }
            catch (AwadException e)
            {
                return Error(e.Message);
            }
        }

        [HttpPost]
        public ActionResult ConfirmVariants(string r, string f, string v)
        {
            try
            {
                var result = _api.ConfirmFare(r, f, v);
                var j = Json(result);
                return Success(j);
            }
            catch (AwadException e)
            {
                return Error(e.Message);
            }
        }

        public ActionResult BuyTickets(string passengers, string r, string f, 
            string v, string email, string phoneCode, string phoneNumber, decimal amount, decimal comission)
        {
            string orderId = string.Empty;
            try
            {
                List<OrderPerson> passngrs =
                    JsonConvert.DeserializeObject<List<OrderPerson>>(passengers).ToList();
                passngrs = passngrs.Where(c => !string.IsNullOrWhiteSpace(c.LastName)).ToList();

                // setup doc exp date
                var setDocExpDate = bool.Parse(ConfigurationManager.AppSettings["SetDocExpDate"]);
                if (setDocExpDate)
                {
                    foreach (var p in passngrs)
                    {
                        p.DocExpDate = "01.01.2030";
                    }
                }

                var orderInfo = new OrderInfo();
                orderInfo.F = f;
                orderInfo.PersonalEmail = email;
                orderInfo.Persons = passngrs;
                orderInfo.PhoneCountry = phoneCode;
                orderInfo.PhoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                orderInfo.R = r;
                orderInfo.UserName = "";
                orderInfo.V = v;
                orderId = _api.CreateOrder(orderInfo);

                // Printing check
                using (var client = new CommandServiceClient())
                {
                    client.PrintCheck(amount, comission);
                }

                return Success(orderId);
            }
            catch (EndpointNotFoundException ex)
            {
#if DEBUG
                return Success(orderId);
#endif
                return Error(ex.Message);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        private GetOrderInfoData GetOrderInfo(string orderId, decimal comission)
        {
            var orderInfo = _api.GetOrderInfo(orderId);

            var created = DateTime.Parse(orderInfo.CreatedDate);
            orderInfo.CreatedDate = created.ToString();

            foreach (var o in orderInfo.Tickets)
            {
                var rcp = _api.GetTicketReceipt(orderId, o.TicketId, comission);
                foreach (var r in rcp.Receipts)
                {
                    var start = DateTime.Parse(r.IssueDate);
                    var end = DateTime.Parse(r.PrintDate);
                    r.IssueDate = start.ToString();
                    r.PrintDate = end.ToString();
                }
                o.Receipt = rcp;
            }

            foreach (var trip in orderInfo.Trips)
            {
                var start = DateTime.Parse(trip.StartTime);
                var end = DateTime.Parse(trip.EndTime);
                trip.StartTime= start.ToString();
                trip.EndTime = end.ToString();
            }

            var log = LogManager.GetCurrentClassLogger();
            var logInfo = JsonConvert.SerializeObject(orderInfo);
            log.Info("OrderInfo: " + logInfo);

            //foreach (var trip in orderInfo.Trips)
            //{
            //    foreach (var rr in r.Receipts)
            //    {
            //         var start = DateTime.Parse(rcp.Trip.StartDate);
            //        var end = DateTime.Parse(rcp.Trip.EndDate);
            //        rr.Trip.StartDate = start.ToString();
            //        rr.Trip.EndDate = end.ToString();
            //    }
            //}

            return orderInfo;
        }

        public ActionResult ShowTickets(string orderId, decimal comission)
        {
            var orderInfo = GetOrderInfo(orderId, comission);

            // Printing tickets
            try
            {
                using (var client = new CommandServiceClient())
                {
                    UrlHelper urlHelper = new UrlHelper(Request.RequestContext);
                    var urlBuilder =
                        new System.UriBuilder(Request.Url.AbsoluteUri)
                        {
                            Path = urlHelper.Action("TicketsToPrint", new {orderId, comission})
                        };

                    Uri uri = urlBuilder.Uri;
                    string url = HttpUtility.UrlDecode(uri.ToString());

                    client.PrintUrl(url);
                }

                return Success(orderInfo);
            }
            catch (EndpointNotFoundException ex)
            {
#if DEBUG
                return Success(orderInfo);
#endif
                throw;
            }
        }

        public ActionResult TicketsToPrint(string orderId, decimal comission)
        {
            var orderInfo = GetOrderInfo(orderId, comission);
            return View(orderInfo);
        }

        private JsonResult Success(object data)
        {
            var serialized = Json(new {Success = true, Data = data});
            return serialized;
        }

        private JsonResult Error(string error)
        {
            return Json(new { Success = false, Error = error });
        }

    }

}
