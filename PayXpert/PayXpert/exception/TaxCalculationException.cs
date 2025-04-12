using System;

namespace PayXpert.exception
{
    public class TaxCalculationException : Exception
    {
        public TaxCalculationException(string message) : base(message) { }
    }
}
