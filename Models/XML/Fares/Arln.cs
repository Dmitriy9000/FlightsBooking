using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.Fares
{
    public class Arln
    {
        [XmlAttribute("N")]
        public string Name { get; set; }

        [XmlAttribute("C")]
        public string C { get; set; }

        [XmlElement("Fare", typeof(Fare))]
        public Fare[] Fare { get; set; }
    }
}