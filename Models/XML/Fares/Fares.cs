using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.Fares
{
    [XmlRoot("Fares")]
    public class Fares
    {
        [XmlElement("Arln", typeof(Arln))]
        public Arln[] Airlines { get; set; }
    }
}