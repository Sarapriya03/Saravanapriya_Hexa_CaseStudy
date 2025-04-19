using System;

namespace PayXpert.exception
{
    public class PayrollGenerationException : Exception
    {
        public PayrollGenerationException(string? message) : base(message)
        {
        }

        public PayrollGenerationException(string message, Exception ex) : base(message) { }
    }
}
