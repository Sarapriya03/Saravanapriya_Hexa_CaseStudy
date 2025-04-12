using System;

namespace PayXpert.exception
{
    public class FinancialRecordException : Exception
    {
        public FinancialRecordException(string message) : base(message) { }
    }
}
