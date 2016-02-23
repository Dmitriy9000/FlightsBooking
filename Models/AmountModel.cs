using System;

namespace PhaseTicket.Models
{
    public static class AmountModel
    {
        private static decimal RoundTo = 1000;

        static AmountModel()
        {
            
        }

        public static decimal CalculateAmount(decimal price)
        {
            if (price < 0)
            {
                throw new Exception("Price can't be lower than 0");
            }

            var t = price / RoundTo;
            var newT = (Math.Truncate(t) + 1) * RoundTo;
            return newT;
        }

        public static decimal CalculateComission(decimal price)
        {
            var amount = CalculateAmount(price);
            return amount - price;
        }
    }
}