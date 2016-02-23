using System;
using System.Xml.Serialization;

namespace PhaseTicket.Models.TicketReceipt
{
    /*
     * <GetTicketReceipt>
<Receipt>
<AgencyName>RUS E TICKETS SA</AgencyName>
<AgencyIATANum>81212740</AgencyIATANum>
<PlatingAircompany>DL</PlatingAircompany>
<Passenger>IVANOV/KONSTANTINMR</Passenger>
<IdentifierNumber>235947315</IdentifierNumber>
<TicketNumber>006 3887234171</TicketNumber>
<IssueDate Year="2009" Month="12" Day="25" Hour="0" Minute="0" DayOfWeek="Friday"/>
<Endorsment>NONREFUNDABLE CHANGE FEE MAY APPLY </Endorsment>
<BaseAmount>USD 73.49</BaseAmount>
<Taxes>CHF 3.00AY 6.00US 9.00XT </Taxes>
<TotalAmount>CHF 95.00</TotalAmount>
<TotalAmountWithMargins>3463.5</TotalAmountWithMargins>
<MarginsAmount>669.27</MarginsAmount>
     * 
<FareCalc>BOS DL NYC 73.49 USD73.49END ZPBOS XT 4.00ZP5.00XF BOS4.5</FareCalc>
<Currency>RUB</Currency>
<PNR>Z0KLQ4</PNR>29
<VendorPNR>CWSIFN</VendorPNR>
<PrintDate Year="2009" Month="12" Day="29" Hour="14" Minute="22" DayOfWeek="Tuesday"/>
     * 
    <Trip Airline="Delta Air Lines" FlightNumber="DL575" Class="U(Economy)" FareBasis="UA21B3NP" 
    Bag="" ST="HK">
         * 
    <Start AirpCode="BOS" AirpName="Logan" City="Boston" Terminal="A"/>
    <End AirpCode="JFK" AirpName="John F Kennedy" City="New York" Terminal="3"/>
    <StartDate>20100311T06:00:00</StartDate>
    <EndDate>20100311T07:29:00</EndDate>
    <NVB>20100311T00:00:00</NVB>
    <NVA>20100311T00:00:00</NVA>
    </Trip>
</Receipt>
</GetTicketReceipt>
     */
    [XmlRoot("GetTicketReceipt")]
    public class TicketReceipt
    {
        [XmlElement("Receipt", typeof(Receipt))]
        public Receipt[] Receipts { get; set; } 
    }

    public class Receipt
    {
        [XmlElement("AgencyName")]
        public string AgencyName { get; set; }

        [XmlElement("AgencyIATANum")]
        public string AgencyIATANum { get; set; }

        [XmlElement("PlatingAircompany")]
        public string PlatingAircompany { get; set; }

        [XmlElement("Passenger")]
        public string Passenger { get; set; }

        [XmlElement("IdentifierNumber")]
        public string IdentifierNumber { get; set; }

        [XmlElement("TicketNumber")]
        public string TicketNumber { get; set; }

        [XmlElement("IssueDate")]
        public string IssueDate { get; set; }

        [XmlElement("Endorsment")]
        public string Endorsment { get; set; }

        [XmlElement("BaseAmount")]
        public string BaseAmount { get; set; }

        [XmlElement("Taxes")]
        public string Taxes { get; set; }

        [XmlElement("TotalAmount")]
        public string TotalAmount { get; set; }

        [XmlElement("TotalAmountWithMargins")]
        public string TotalAmountWithMargins { get; set; }

        [XmlElement("MarginsAmount")]
        public string MarginsAmount { get; set; }

        [XmlElement("FareCalc")]
        public string FareCalc { get; set; }

        [XmlElement("Currency")]
        public string Currency { get; set; }

        [XmlElement("PNR")]
        public string PNR { get; set; }

        [XmlElement("VendorPNR")]
        public string VendorPNR { get; set; }

        [XmlElement("PrintDate")]
        public string PrintDate { get; set; }

        [XmlElement("Trip", typeof(ReceiptTrip))]
        public ReceiptTrip Trip { get; set; }
    }

    /* <IssueDate Year="2009" Month="12" Day="25" Hour="0" Minute="0" DayOfWeek="Friday"/> */
    public class ReceiptDate
    {
        [XmlAttribute("Year")]
        public int Year { get; set; }

        [XmlAttribute("Month")]
        public int Month { get; set; }

        [XmlAttribute("Day")]
        public int Day { get; set; }

        [XmlAttribute("Hour")]
        public int Hour { get; set; }

        [XmlAttribute("Minute")]
        public int Minute { get; set; }

        [XmlAttribute("DayOfWeek")]
        public DayOfWeek DayOfWeek { get; set; }
    }

    /*
     * <Trip Airline="Delta Air Lines" FlightNumber="DL575" Class="U(Economy)" FareBasis="UA21B3NP" Bag="" ST="HK">
            <Start AirpCode="BOS" AirpName="Logan" City="Boston" Terminal="A"/>
            <End AirpCode="JFK" AirpName="John F Kennedy" City="New York" Terminal="3"/>
            <StartDate>20100311T06:00:00</StartDate>
            <EndDate>20100311T07:29:00</EndDate>
            <NVB>20100311T00:00:00</NVB>
            <NVA>20100311T00:00:00</NVA>
    </Trip>
     */
    public class ReceiptTrip
    {
        [XmlAttribute("Airline")]
        public string Airline { get; set; }
        [XmlAttribute("FlightNumber")]
        public string FlightNumber { get; set; }
        [XmlAttribute("Class")]
        public string Class { get; set; }
        [XmlAttribute("FareBasis")]
        public string FareBasis { get; set; }
        [XmlAttribute("Bag")]
        public string Bag { get; set; }
        [XmlAttribute("ST")]
        public string ST { get; set; }

        [XmlElement("Start", typeof(ReceiptTripPoint))]
        public ReceiptTripPoint Start { get; set; }
        [XmlElement("End", typeof(ReceiptTripPoint))]
        public ReceiptTripPoint End { get; set; }

        [XmlElement("StartDate")]
        public string StartDate { get; set; }
        [XmlElement("EndDate")]
        public string EndDate { get; set; }
        [XmlElement("NVB")]
        public string NVB { get; set; }
        [XmlElement("NVA")]
        public string NVA { get; set; }
    }

    /* <Start AirpCode="BOS" AirpName="Logan" City="Boston" Terminal="A"/> */
    public class ReceiptTripPoint
    {
        [XmlAttribute("AirpCode")]
        public string AirpCode { get; set; }
        [XmlAttribute("AirpName")]
        public string AirpName { get; set; }
        [XmlAttribute("City")]
        public string City { get; set; }
        [XmlAttribute("Terminal")]
        public string Terminal { get; set; }
    }
}