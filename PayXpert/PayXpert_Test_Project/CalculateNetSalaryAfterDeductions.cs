using NUnit.Framework;
using System;

namespace PayXpert_Test_Project
{
    public class PayrollCalculatorTests1
    {
        private PayrollCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new PayrollCalculator();
        }

        [Test]
        public void CalculateNetSalary_ValidInputs_ReturnsCorrectNetSalary()
        {
            // Arrange
            decimal grossSalary = 50000m;
            decimal deductions = 7000m;
            decimal expectedNetSalary = 43000m;

            // Act
            decimal actualNetSalary = _calculator.CalculateNetSalary(grossSalary, deductions);

            // Debug info
            Console.WriteLine($"Expected: {expectedNetSalary}, Actual: {actualNetSalary}");

            // Assert
            Assert.That(actualNetSalary, Is.EqualTo(expectedNetSalary).Within(0.01m));
        }


        [Test]
        public void CalculateNetSalary_ZeroDeductions_ReturnsGrossSalary()
        {
            // Arrange
            decimal grossSalary = 40000m;
            decimal deductions = 0m;
            decimal expectedNetSalary = 40000m;

            // Act
            decimal actualNetSalary = _calculator.CalculateNetSalary(grossSalary, deductions);

            // Assert
            Assert.AreEqual(expectedNetSalary, actualNetSalary);
        }

        [Test]
        public void CalculateNetSalary_DeductionsGreaterThanGross_ThrowsException()
        {
            // Arrange
            decimal grossSalary = 30000m;
            decimal deductions = 35000m;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                _calculator.CalculateNetSalary(grossSalary, deductions)
            );

            Assert.That(ex.Message, Is.EqualTo("Deductions cannot exceed gross salary"));
        }
    }
}


