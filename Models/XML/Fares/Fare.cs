using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.Fares
{
    public class Fare
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("AT")]
        public string Price { get; set; }

        [XmlAttribute("Avl")]
        public bool Available { get; set; }

        [XmlAttribute("Sts")]
        public string Seats { get; set; }

        [XmlElement("Dir", typeof(Dir))]
        public Dir[] Dir { get; set; }

        public FareDetails.FareDetails Additional { get; set; }

        // Price with our comission
        public decimal PriceWithComission { get; set; }

        // Comission
        public decimal Comission { get; set; }
    }
}