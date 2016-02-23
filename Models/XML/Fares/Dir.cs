using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.Fares
{
    public class Dir
    {
        [XmlAttribute("DepApt")]
        public string DepartureAirport { get; set; }

        [XmlAttribute("ArrApt")]
        public string ArrivalAirport { get; set; }

        [XmlAttribute("DepTm")]
        public string DeprtureTime { get; set; }

        [XmlAttribute("ArrTm")]
        public string ArrivalTime { get; set; }

        [XmlAttribute("BrdCng")]
        public string BrdCng { get; set; }

        [XmlAttribute("FltNum")]
        public string FlightNumber { get; set; }

        [XmlAttribute("Hr")]
        public int Hours { get; set; }

        [XmlAttribute("Min")]
        public int Minutes { get; set; }
    }
}