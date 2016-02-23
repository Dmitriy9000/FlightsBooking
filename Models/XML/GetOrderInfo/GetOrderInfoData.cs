using System;

namespace PhaseTicket.Models.XML.GetOrderInfo
{
    public class GetOrderInfoData
    {
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
        public string OrderNumber { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string PresentScore { get; set; }
        public string ScoreForOrder { get; set; }
        public string PaymentMethod { get; set; }
        public string TimeLimitUTCString { get; set; }

        public OrderInfoTicket[] Tickets { get; set; }

        public OrderInfoTrip[] Trips { get; set; }
    }

    public class OrderInfoTicket
    {
        public string TicketId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string TicketNumber { get; set; }
        public string Passport { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Number { get; set; }
        public string BirthDate { get; set; }
        public string FrequentlyFlyerCard { get; set; }
        public string OccupiesSeparateSeat { get; set; }
        public TicketReceipt.TicketReceipt Receipt { get; set; }

        //public string ChangeRequests { get; set; }
        //public string ReturnRequests { get; set; }
    }

    public class OrderInfoTrip
    {
        public Guid TripId { get; set; }
        public int TripNumber { get;set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string Carrier { get; set; }
        public string FlightNumber { get; set; }
        public bool Conx { get; set; }
        public string Direction { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get;set; }
        public string TimeOnEarth { get; set; }
        public string Arrive { get; set; }
        public string Departure { get; set; }
        public string JourneyTime { get;set; }
        public string FlightTime { get; set; }

        public TicketNameCode StartCity { get; set; }
        public TicketNameCode EndCity { get; set; }
        public TicketNameCode AirCompany { get; set; }
        public TicketNameCode StartAirp { get; set; }
        public TicketNameCode EndAirp { get; set; }
        public TicketNameCode Airplane { get; set; }
        public TicketClass Class { get; set; }
        
    }

    public class TicketClass
    {
        public string ClassName { get; set; }
        public string Class { get; set; }
        public string ServiceClass { get; set; }
    }

    public class TicketNameCode
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}