using System;

namespace PayXpert.exception
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message, Microsoft.Data.SqlClient.SqlException ex) : base(message) { }
    }
}
