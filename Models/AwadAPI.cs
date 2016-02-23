using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using NLog;
using PhaseTicket.Models.XML.ConfirmFare;
using PhaseTicket.Models.XML.CreateOrder;
using PhaseTicket.Models.XML.FareDetails;
using PhaseTicket.Models.XML.FareRules;
using PhaseTicket.Models.XML.Fares;
using PhaseTicket.Models.XML.GetOrderInfo;

namespace PhaseTicket.Models
{
    public class AwadAPI
    {
        private readonly string _host;
        private readonly string _code;
        private Logger Log;

        public AwadAPI()
        {
            _host = ConfigurationManager.AppSettings["AwadHost"];
            _code = ConfigurationManager.AppSettings["AwadCode"];
            Log = LogManager.GetCurrentClassLogger();
        }

        public string InitRequest(List<TravelEpisode> points, int amateurs, int childs, int infants, ServiceClass serviceClass)
        {
            if (points.Count > 4)
                throw new Exception("Only 4 waypoints allowed");
            
            var r = string.Empty;
            foreach (var travelPoint in points)
            {
                r += string.Format("{0}{1}{2}{3}", 
                    travelPoint.DepartureDate.Day.ToString("00"), travelPoint.DepartureDate.Month.ToString("00"),
                    travelPoint.DeparturePoint, travelPoint.ArrivePoint);
            }

            r += "&AD=" + amateurs;
            r += "&CN=" + childs;
            r += "&IN=" + infants;
            r += "&SC=" + serviceClass;

            Log.Info(r);

            //0512MOWLON1212LONMOW&AD=2&CN=1&SC=B
            return InitRequest(r);
        }

        public string InitRequest(string request)
        {
            var r = string.Format("{0}/api/NewRequest/?Route={1}&Partner={2}", _host, request, _code);
            var doc = new XmlDocument();
            doc.Load(r);

            var id = doc.DocumentElement.GetAttribute("Id");
            var error = doc.DocumentElement.GetAttribute("Error");

            Log.Info(r);

            if (!string.IsNullOrWhiteSpace(id))
            {
                return id;
            }

            throw new AwadException("InitRequest: " + error);
        }

        public string GetRequestInfo(string id)
        {
            var r = string.Format("{0}/api/RequestInfo/?R={1}&language=RU", _host, id);
            var doc = new XmlDocument();
            doc.Load(r);

            return doc.OuterXml;
        }

        public string SearchStatus(string id)
        {
            var infoTest = GetRequestInfo(id);

            var r = string.Format("{0}/api/RequestState/?R={1}", _host, id);
            var doc = new XmlDocument();
            doc.Load(r);

            var completed = doc.DocumentElement.GetAttribute("Completed");
            var error = doc.DocumentElement.GetAttribute("Error");

            Log.Info(r);

            if (string.IsNullOrWhiteSpace(error))
            {
                return completed;
            }

            throw new AwadException("SearchStatus: " + error);
        }

        public Fares Fares(string id)
        {
            

            var r = string.Format("{0}/api/Fares/?R={1}&V=Matrix", _host, id);
            var doc = new XmlDocument();
            doc.Load(r);

            Log.Info(r);
            Log.Info(doc.OuterXml);

            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                var serializer = new XmlSerializer(typeof(Fares));
                var tr = new StringReader(doc.InnerXml);
                var reader = new XmlTextReader(tr);
                var airlines = (Fares)serializer.Deserialize(reader);
                reader.Close();
                return airlines;
            }

            throw new AwadException("Fares: " + error);
        }

        public FareDetails FareDetails(string requestId, string fareId)
        {
            var r = string.Format("{0}/api/Fare/?R={1}&F={2}", _host, requestId, fareId);
            var doc = new XmlDocument();
            doc.Load(r);

            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                var serializer = new XmlSerializer(typeof (FareDetails));
                var tr = new StringReader(doc.InnerXml);
                var reader = new XmlTextReader(tr);
                var airlines = (FareDetails) serializer.Deserialize(reader);
                reader.Close();
                return airlines;
            }

            throw new AwadException("FareDetails: " + error);
        }

        /// <summary>
        /// Fares with preloaded details for each Fair
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public Fares FareWithDetails(string requestId)
        {
            var culture = new System.Globalization.CultureInfo("ru-RU");

            var result = Fares(requestId);

            int did = 0;

            // Additional info (start and end time and date, flight number, time in air)
            foreach (var airline in result.Airlines)
            {
                foreach (var fare in airline.Fare)
                {
                    fare.Additional = FareDetails(requestId, fare.Id);

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

            return result;
        }

        /// <summary>Правила тарифа</summary>
        public FareRules FareRules(string requestId, string fareId)
        {
            var r = string.Format("{0}/api/FareRules/?R={1}&F={2}", _host, requestId, fareId);
            var doc = new XmlDocument();
            doc.Load(r);

            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                var serializer = new XmlSerializer(typeof (FareRules));
                var tr = new StringReader(doc.InnerXml);
                var reader = new XmlTextReader(tr);
                var airlines = (FareRules)serializer.Deserialize(reader);
                airlines.ReplaceBreakLines();
                reader.Close();
                return airlines;
            }

            throw new AwadException("FareRules: " + error);
        }

        /// <summary>Доступность выбранных вариантов</summary>
        public ConfirmFare ConfirmFare(string requestId, string fareId, string variant)
        {
            var r = string.Format("{0}/api/ConfirmFare/?R={1}&F={2}&V={3}", _host, requestId, fareId, variant);
            var doc = new XmlDocument();
            doc.Load(r);

            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                var serializer = new XmlSerializer(typeof(ConfirmFare));
                var tr = new StringReader(doc.InnerXml);
                var reader = new XmlTextReader(tr);
                var airlines = (ConfirmFare)serializer.Deserialize(reader);
                reader.Close();
                return airlines;
            }

            throw new AwadException("ConfirmFare: " + error);
        }

        /// <summary>Создание заказа</summary>
        public string CreateOrder(OrderInfo info)
        {
            /* {host}/api/ 
CreateOrder/?R=245a02R1SMmA19&F=50&V=0;2&FName1=IVAN&LName1=IVANOV&PCountry1=RU&BDate1=27.01.1980&G1=M&PNumber=11111&PExpDate=27.01.2011&Phone=9161234567&PhoneCountry=RU|7&UserNa
me=myprofile@email.com&PersonalEmail=myemail@email.com 
             
             &FName1=IVAN&LName1=IVANOV&PCountry1=RU&BDate1=27.01.1980&G1=M&PNumber=11111&PExpDate=27.01.2011
             
             */
            var persons = string.Empty;
            for (int i = 0; i < info.Persons.Count; i++)
            {
                var pers = info.Persons[i];
                persons +=
                    string.Format(
                        "&FName{0}={1}&LName{0}={2}&PCountry{0}={3}&BDate{0}={4}&G{0}={5}&PNumber{0}={6}&PExpDate{0}={7}",
                        i + 1, pers.FirstName, pers.LastName, pers.Country, pers.Dob, pers.Sex, pers.DocNumber,
                        pers.DocExpDate);
            }

            var request =
                string.Format(
                    "{0}/api/CreateOrder/?R={1}&F={2}&V={3}&Phone={4}&PhoneCountry={5}&UserName={7}&PersonalEmail={7}{8}",
                    _host, info.R, info.F, info.V, info.PhoneNumber, info.PhoneCountry, info.UserName,
                    info.PersonalEmail, persons);

            Log.Info(request);

            var doc = new XmlDocument();
            doc.Load(request);
            var id = doc.DocumentElement.GetAttribute("OrderId");
            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                return id;
            }

            throw new AwadException("CreateOrder: " + error);
        }

        /// <summary>Статус создания заказа</summary>
        public bool CreateOrderStatusSuccess(string orderId)
        {
            var r = string.Format("{0}/api/GetCreateOrderStatus/?OrderProcessId={1}", _host, orderId);
            var doc = new XmlDocument();
            doc.Load(r);
            var id = doc.DocumentElement.GetAttribute("Id");
            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                return true;
            }

            return false;
        }

        /// <summary>Информация о статусе заказа</summary>
        public GetOrderInfoData GetOrderInfo(string orderId)
        {
            var r = string.Format("{0}/api/GetOrderInfo2/?orderId={1}", _host, orderId);
            var client = new WebClient();
            var data = client.DownloadString(r);

            Log.Info("Order info:");
            Log.Info(data);

            var passngrs = Newtonsoft.Json.JsonConvert.DeserializeObject<GetOrderInfoData>(data);
            return passngrs;

            //throw new AwadException("GetOrderInfo: " + error);
        }

        /// <summary>Запрос маршрут квитанции</summary>
        public TicketReceipt.TicketReceipt GetTicketReceipt(string orderId, string ticketId, decimal fee)
        {
            var r = string.Format("{0}/api/GetTicketReceipt/?OrderId={1}&TicketId={2}&Fee={3}", _host, orderId, ticketId, fee);
            var doc = new XmlDocument();
            doc.Load(r);

            Log.Info("Ticket receipt: {0}", r);
            Log.Info(doc.OuterXml);

            var error = doc.DocumentElement.GetAttribute("Error");

            if (string.IsNullOrWhiteSpace(error))
            {
                var serializer = new XmlSerializer(typeof(TicketReceipt.TicketReceipt));
                var tr = new StringReader(doc.InnerXml);
                var reader = new XmlTextReader(tr);
                var airlines = (TicketReceipt.TicketReceipt)serializer.Deserialize(reader);
                reader.Close();
                return airlines;
            }

            throw new AwadException("GetTicketReceipt: " + error);
        }
    }

    public class TravelEpisode
    {
        public string DeparturePoint { get; set; }
        public string ArrivePoint { get; set; }
        public DateTime DepartureDate { get; set; }
    }

    public enum ServiceClass
    {
        E, // econom
        B // business
    }
}