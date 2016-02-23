using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhaseTicket.Models.Dicts;

namespace PhaseTicket.Controllers
{
    public class TypeaheadController : Controller
    {
        private readonly AirportsAndCities _airportsAndCities;

        public TypeaheadController(AirportsAndCities airportsAndCities)
        {
            _airportsAndCities = airportsAndCities;
        }

        public ActionResult TravelPoints(string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                var info =
                    _airportsAndCities.GetSuggest(term)
                        .Select(c => new TypeaheadItem() {id = c.Code, value = c.NameRu + ", " + c.CountryName})
                        .ToArray();
                return Json(info, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }

    public struct TypeaheadItem
    {
        public string id { get; set; }
        public string value { get; set; }
    }

}
