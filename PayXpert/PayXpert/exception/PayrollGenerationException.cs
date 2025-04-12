using System;

namespace PayXpert.exception
{
    public class PayrollGenerationException : Exception
    {
        public PayrollGenerationException(string message) : base(message) { }
    }
}
