using System;

namespace PayXpert.exception
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() : base() { }
        public EmployeeNotFoundException(string message) : base(message) { }
        public EmployeeNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
