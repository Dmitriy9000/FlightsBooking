using System;

namespace PhaseTicket.Models
{
    public class AwadException : Exception
    {
        public AwadException(string message): base(message) { }
    }
}