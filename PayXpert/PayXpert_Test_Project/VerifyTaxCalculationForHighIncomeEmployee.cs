using NUnit.Framework;
using System;

namespace PayXpert_Test_Project
{
    public class PayrollCalculatorTests2
    {
        private PayrollCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new PayrollCalculator();
        }

        [Test]
        public void VerifyTaxCalculationForHighIncomeEmployee()
        {
            // Arrange
            decimal highIncome = 1500000m; // 15 Lakhs
            decimal expectedTax = 300000m; // 20% of 15L

            // Act
            decimal actualTax = _calculator.CalculateTax(highIncome);

            // Assert
            Assert.AreEqual(expectedTax, actualTax);
        }

        [Test]
        public void CalculateTax_NegativeIncome_ThrowsException()
        {
            // Arrange
            decimal income = -100000m;

            // Assert + Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _calculator.CalculateTax(income)
            );

            Assert.That(ex.Message, Is.EqualTo("Income cannot be negative"));
        }
    }
}

