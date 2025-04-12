using System.Collections.Generic;
using PayXpert.entity;

namespace PayXpert.dao
{
    public interface ITaxService
    {
        Tax CalculateTax(int employeeId, int taxYear);
        Tax GetTaxById(int taxId);
        List<Tax> GetTaxesForEmployee(int employeeId);
        List<Tax> GetTaxesForYear(int taxYear);
    }
}
