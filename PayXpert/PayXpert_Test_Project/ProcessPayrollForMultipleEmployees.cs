using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PayXpert_Test_Project
{
    public class PayrollProcessorTests
    {
        private PayrollProcessor _processor;

        [SetUp]
        public void Setup()
        {
            _processor = new PayrollProcessor();
        }

        [Test]
        public void ProcessPayrollForMultipleEmployees_ValidInputs_ReturnsCorrectPayrolls()
        {
            // Arrange
            var employees = new List<EmployeePayrollInput>
            {
                new EmployeePayrollInput { EmployeeID = 1, BasicSalary = 30000m, Allowances = 5000m, OvertimePay = 2000m, Deductions = 4000m },
                new EmployeePayrollInput { EmployeeID = 2, BasicSalary = 40000m, Allowances = 6000m, OvertimePay = 2500m, Deductions = 5500m },
                new EmployeePayrollInput { EmployeeID = 3, BasicSalary = 35000m, Allowances = 4000m, OvertimePay = 1000m, Deductions = 3000m }
            };

            // Act
            List<ProcessedPayroll> results = _processor.ProcessBatch(employees);

            // Assert
            Assert.AreEqual(3, results.Count);

            Assert.AreEqual(37000m, results[0].GrossSalary);
            Assert.AreEqual(33000m, results[0].NetSalary);

            Assert.AreEqual(48500m, results[1].GrossSalary);
            Assert.AreEqual(43000m, results[1].NetSalary);

            Assert.AreEqual(40000m, results[2].GrossSalary);
            Assert.AreEqual(37000m, results[2].NetSalary);
        }
    }
    public class EmployeePayrollInput
    {
        public int EmployeeID { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Deductions { get; set; }
    }
    public class ProcessedPayroll
    {
        public int EmployeeID { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
    }
    public class PayrollProcessor
    {
        public List<ProcessedPayroll> ProcessBatch(List<EmployeePayrollInput> employeeInputs)
        {
            var result = new List<ProcessedPayroll>();

            foreach (var emp in employeeInputs)
            {
                if (emp.OvertimePay < 0 || emp.Deductions < 0)
                    throw new ArgumentException("Overtime pay and deductions must be non-negative.");

                decimal grossSalary = emp.BasicSalary + emp.Allowances + emp.OvertimePay;
                decimal netSalary = grossSalary - emp.Deductions;

                result.Add(new ProcessedPayroll
                {
                    EmployeeID = emp.EmployeeID,
                    GrossSalary = grossSalary,
                    NetSalary = netSalary
                });
            }

            return result;
        }
    }
}
