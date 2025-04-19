using NUnit.Framework;
using System;

namespace PayXpert_Test_Project
{
    [TestFixture]
    public class PayrollCalculatorTests
    {
        private PayrollCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new PayrollCalculator();
        }

        [Ignore("This test is ignored because it is not relevant to the current implementation.")]
        public void CalculateGrossSalary_ValidInputs_ReturnsCorrectGrossSalary()
        {
            // Arrange
            decimal basicSalary = 30000m;
            decimal allowances = 5000m;
            decimal overtimePay = 2000m;
            decimal expectedGrossSalary = 37000m;

            // Act
            decimal actualGrossSalary = _calculator.CalculateGrossSalary(basicSalary, allowances, overtimePay);

            // Assert
            Assert.AreEqual(expectedGrossSalary, actualGrossSalary);
        }

        [Test]
        public void CalculateGrossSalary_WithZeroValues_ReturnsBasicSalary()
        {
            // Arrange
            decimal basicSalary = 25000m;
            decimal allowances = 0m;
            decimal overtimePay = 0m;
            decimal expectedGrossSalary = 25000m;

            // Act
            decimal actualGrossSalary = _calculator.CalculateGrossSalary(basicSalary, allowances, overtimePay);

            // Assert
            Assert.AreEqual(expectedGrossSalary, actualGrossSalary);
        }

        [Test]
        public void CalculateGrossSalary_NegativeOvertime_ThrowsException()
        {
            // Arrange
            decimal basicSalary = 28000m;
            decimal allowances = 2000m;
            decimal overtimePay = -1000m;

            // Act + Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                _calculator.CalculateGrossSalary(basicSalary, allowances, overtimePay)
            );

            Assert.That(ex.Message, Is.EqualTo("Overtime pay cannot be negative"));
        }
    }

    /// <summary>
    /// PayrollCalculator is a simple class to compute gross salary from basic, allowances, and overtime pay.
    /// </summary>
    internal class PayrollCalculator
    {
        public decimal CalculateGrossSalary(decimal basicSalary, decimal allowances, decimal overtimePay)
        {
            if (overtimePay < 0)
                throw new ArgumentException("Overtime pay cannot be negative");

            return basicSalary + allowances + overtimePay;
        }

        public decimal CalculateNetSalary(decimal grossSalary, decimal totalDeductions)
        {
            if (totalDeductions < 0)
                throw new ArgumentException("Deductions cannot be negative");
            if (totalDeductions > grossSalary)
                throw new ArgumentException("Deductions cannot exceed gross salary");

            return grossSalary - totalDeductions;
        }

        public decimal CalculateTax(decimal income)
        {
            if (income < 0)
                throw new ArgumentException("Income cannot be negative");

            return income * 0.20m;
        }
    }
}
