using System;
using System.Collections.Generic;
using System.Linq;
using PayXpert.entity;
using PayXpert.exception;

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
            try
            {
                var employees = _employeeService.GetAllEmployees();
                return employees.ToDictionary(e => e.EmployeeID, e => e.FirstName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching employee names: " + ex.Message);
                return new Dictionary<int, string>();
            }
        }

        public void GeneratePayrollReport(List<Payroll> payrolls)
        {
            try
            {
                var employeeNames = GetEmployeeNames();

                var invalidIds = payrolls
                    .Select(p => p.EmployeeID)
                    .Distinct()
                    .Where(id => !employeeNames.ContainsKey(id))
                    .ToList();

                if (invalidIds.Any())
                {
                    throw new EmployeeNotFoundException($"Employee(s) with ID(s) {string.Join(", ", invalidIds)} not found.");
                }

                Console.WriteLine("\n--- Payroll Report ---");
                foreach (var p in payrolls)
                {
                    string name = employeeNames[p.EmployeeID];
                    Console.WriteLine($"EmployeeID: {p.EmployeeID} | Name: {name} | Pay Period: {p.PayPeriodStartDate:yyyy-MM-dd} to {p.PayPeriodEndDate:yyyy-MM-dd} | Net Salary: ₹{p.NetSalary:F2}");
                }
            }
            catch (EmployeeNotFoundException enfe)
            {
                Console.WriteLine("Error: " + enfe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating payroll report: " + ex.Message);
            }
        }

        public void GenerateTaxReport(List<Tax> taxes)
        {
            try
            {
                var employeeNames = GetEmployeeNames();

                var invalidIds = taxes
                    .Select(t => t.EmployeeID)
                    .Distinct()
                    .Where(id => !employeeNames.ContainsKey(id))
                    .ToList();

                if (invalidIds.Any())
                {
                    throw new EmployeeNotFoundException($"Employee(s) with ID(s) {string.Join(", ", invalidIds)} not found.");
                }

                Console.WriteLine("\n--- Tax Report ---");
                foreach (var t in taxes)
                {
                    string name = employeeNames[t.EmployeeID];
                    Console.WriteLine($"EmployeeID: {t.EmployeeID} | Name: {name} | Tax Year: {t.TaxYear} | Taxable Income: ₹{t.TaxableIncome:F2} | Tax Amount: ₹{t.TaxAmount:F2}");
                }
            }
            catch (EmployeeNotFoundException enfe)
            {
                Console.WriteLine("Error: " + enfe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating tax report: " + ex.Message);
            }
        }

        public void GenerateFinancialSummary(List<FinancialRecord> records)
        {
            try
            {
                var employeeNames = GetEmployeeNames();

                var invalidIds = records
                    .Select(r => r.EmployeeID)
                    .Distinct()
                    .Where(id => !employeeNames.ContainsKey(id))
                    .ToList();

                if (invalidIds.Any())
                {
                    throw new EmployeeNotFoundException($"Employee(s) with ID(s) {string.Join(", ", invalidIds)} not found.");
                }

                Console.WriteLine("\n--- Financial Summary ---");
                foreach (var r in records)
                {
                    string name = employeeNames[r.EmployeeID];
                    Console.WriteLine($"EmployeeID: {r.EmployeeID} | Name: {name} | Date: {r.RecordDate:yyyy-MM-dd} | Type: {r.RecordType} | Description: {r.Description} | Amount: ₹{r.Amount:F2}");
                }
            }
            catch (EmployeeNotFoundException enfe)
            {
                Console.WriteLine("Error: " + enfe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating financial summary: " + ex.Message);
            }
        }
    }
}
