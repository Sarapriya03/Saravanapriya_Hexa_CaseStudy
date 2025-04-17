using System;
using System.Collections.Generic;
using System.Linq;
using PayXpert.entity;

namespace PayXpert.dao
{
    public class ReportGenerator
    {
        private readonly EmployeeService _employeeService;

        public ReportGenerator(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        private Dictionary<int, string> GetEmployeeNames()
        {
            var employees = _employeeService.GetAllEmployees();
            return employees.ToDictionary(e => e.EmployeeID, e => e.FirstName); 
        }

        public void GeneratePayrollReport(List<Payroll> payrolls)
        {
            var employeeNames = GetEmployeeNames();

            Console.WriteLine("\n--- Payroll Report ---");
            foreach (var p in payrolls)
            {
                string name = employeeNames.ContainsKey(p.EmployeeID) ? employeeNames[p.EmployeeID] : "Unknown";
                Console.WriteLine($"EmployeeID: {p.EmployeeID} | Name: {name} | Pay Period: {p.PayPeriodStartDate:yyyy-MM-dd} to {p.PayPeriodEndDate:yyyy-MM-dd} | Net Salary: ₹{p.NetSalary:F2}");
            }
        }

        public void GenerateTaxReport(List<Tax> taxes)
        {
            var employeeNames = GetEmployeeNames();

            Console.WriteLine("\n--- Tax Report ---");
            foreach (var t in taxes)
            {
                string name = employeeNames.ContainsKey(t.EmployeeID) ? employeeNames[t.EmployeeID] : "Unknown";
                Console.WriteLine($"EmployeeID: {t.EmployeeID} | Name: {name} | Tax Year: {t.TaxYear} | Taxable Income: ₹{t.TaxableIncome:F2} | Tax Amount: ₹{t.TaxAmount:F2}");
            }
        }

        public void GenerateFinancialSummary(List<FinancialRecord> records)
        {
            var employeeNames = GetEmployeeNames();

            Console.WriteLine("\n--- Financial Summary ---");
            foreach (var r in records)
            {
                string name = employeeNames.ContainsKey(r.EmployeeID) ? employeeNames[r.EmployeeID] : "Unknown";
                Console.WriteLine($"EmployeeID: {r.EmployeeID} | Name: {name} | Date: {r.RecordDate:yyyy-MM-dd} | Type: {r.RecordType} | Description: {r.Description} | Amount: ₹{r.Amount:F2}");
            }
        }
    }
}
