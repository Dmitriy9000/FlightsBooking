using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhaseTicket.Models.XML.CreateOrder
{
    public class OrderInfo
    {
        /*  PhoneNumber - номер телефона покупателя
  PhoneCountry - двух-буквенный и телефонный код страны, разделенные символом «|»
  UserName – Email клиента
  PersonalEmail –
         R – ключ поискового запроса;
  F – ключ тарифа;
  V – выбранные варианты;  * 
         */

        public string R { get; set; }
        public string F { get; set; }
        public string V { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneCountry { get; set; }
        public string UserName { get; set; }
        public string PersonalEmail { get; set; }

        public List<OrderPerson> Persons { get; set; }

        public OrderInfo()
        {
            Persons = new List<OrderPerson>();
        }
    }
}