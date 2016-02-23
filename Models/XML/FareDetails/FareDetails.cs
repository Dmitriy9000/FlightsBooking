using System;
using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.FareDetails
{
    [XmlRoot("Fare")]
    public class FareDetails
    {
        [XmlAttribute("Currency")]
        public string Currency { get; set; }        [XmlAttribute("TotalAmount")]        public int TotalAmount { get; set; }        [XmlAttribute("TotalAmountNonCeiled")]        public decimal TotalAmountNonCeiled { get; set; }        [XmlAttribute("Available")]        public string Available { get; set; }        [XmlAttribute("MinAvailSeats")]        public string MinAvailSeats { get; set; }        public FareDetailsAmount Amount { get; set; }

        [XmlElement("Dir", typeof(FareDetailsDir))]
        public FareDetailsDir[] Dirs { get; set; }

        public string Airline { get; set; }
    }

    public class FareDetailsAmount
    {
        [XmlAttribute("ABase")]
        public int AdultsBase { get; set; }
        [XmlAttribute("ATaxes")]
        public int AdultTaxes { get; set; }
        [XmlAttribute("ATotal")]
        public int AdultTotal { get; set; }

        [XmlAttribute("CBase")]
        public int ChildsBase { get; set; }
        [XmlAttribute("CTaxes")]
        public int ChildsTaxes { get; set; }
        [XmlAttribute("CTotal")]
        public int ChildsTotal { get; set; }

        [XmlAttribute("IBase")]
        public int InfantsBase { get; set; }
        [XmlAttribute("ITaxes")]
        public int InfantsTaxes { get; set; }
        [XmlAttribute("ITotal")]
        public int InfantsTotal { get; set; }
    }

    public class FareDetailsDir
    {
        [XmlElement("Variant", typeof(FareDetailsVariant))]
        public FareDetailsVariant[] Variants { get; set; }

        public string Id { get; set; }
    }

    public class FareDetailsVariant
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("Leg", typeof(FareDetailsLeg))]
        public FareDetailsLeg[] Legs { get; set; }
    }

    public class FareDetailsLeg
    {
        public FareDetailsPlane Plane { get; set; }
        public FareDetailsPoint Departure { get; set; }
        public FareDetailsPoint Arrival { get; set; }
    }

    public class FareDetailsPlane
    {
        [XmlAttribute("C")]
        public string C { get; set; }

        [XmlAttribute("N")]
        public string Name { get; set; }
    }

    /* <Departure Code="VKO" Contry="Россия" City="Москва" Airport="Внуково" Terminal="" Date="2011-05-04" Time="15:05" DayOfWeek="Wednesday" /> */
    public class FareDetailsPoint
    {
        [XmlAttribute("Code")]
        public string Code { get; set; }
        [XmlAttribute("Contry")]
        public string Country { get; set; }
        [XmlAttribute("City")]
        public string City { get; set; }
        [XmlAttribute("Airport")]
        public string Airport { get; set; }
        [XmlAttribute("Terminal")]
        public string Terminal { get; set; }
        [XmlAttribute("Date")]
        public string Date { get; set; }
        [XmlAttribute("Time")]
        public string Time { get; set; }
        [XmlAttribute("DayOfWeek")]
        public string DayOfWeek { get; set; }
    }
}