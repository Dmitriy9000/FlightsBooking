
namespace PhaseTicket.Models.XML.CreateOrder
{
    /*
     * FName1 – имя первого пассажира
LName1– фамилия первого пассажира
PCountry1 – гражданство указанное в паспорте
BDate1 – дата рождения
PNumber1 – номер документа
PExpDate1 – дата окончания срока действия документа
G1 – пол (M / F)
FrequentFlyerAirline1 – Код авиакомпании, если есть бонусная карточка авиакомпании
FrequentFlyerNumber 1 – Номер бонусной карты, если есть бонусная карточка авиакомпании
Аналогичные параметры с постфиксом 2, 3
     */
    public class OrderPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Country { get; set; }
        public string Dob { get; set; }
        public string DocNumber { get; set; }
        public string DocExpDate { get; set; }
        public Gender Sex { get; set; }
        public string CardAirlines { get; set; }
        public string CardAirlineNumber { get; set; }
    }

    public enum Gender
    {
        M,
        F
    }
}