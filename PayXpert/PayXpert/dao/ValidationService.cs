using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.dao
{
    public class ValidationService
    {
        public bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        public bool IsPositiveAmount(decimal amount)
        {
            return amount >= 0;
        }

        internal bool IsValidEmail(object email)
        {
            throw new NotImplementedException();
        }
    }
}
