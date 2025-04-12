using System;
using System.Collections.Generic;
using PayXpert.entity;

namespace PayXpert.dao
{
    public interface IPayrollService
    {
        Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);
        Payroll GetPayrollById(int payrollId);
        List<Payroll> GetPayrollsForEmployee(int employeeId);
        List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
