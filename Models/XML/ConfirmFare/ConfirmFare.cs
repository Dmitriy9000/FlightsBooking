using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.ConfirmFare
{
    /* <ConfirmFare R="253ygQh58uRc61" F="198" Confirmed="True" MinAvailSeats="4"/> */
    public class ConfirmFare
    {
        [XmlAttribute("R")]
        public string R { get; set; }
        [XmlAttribute("F")]
        public string F { get; set; }
        [XmlAttribute("Confirmed")]
        public string Confirmed { get; set; }
        [XmlAttribute("MinAvailSeats")]
        public string MinAvailableSeats { get; set; }
    }
}