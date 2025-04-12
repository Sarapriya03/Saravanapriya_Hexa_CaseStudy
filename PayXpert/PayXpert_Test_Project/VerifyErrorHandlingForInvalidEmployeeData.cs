using NUnit.Framework;
using System;

namespace PayXpert_Test_Project
{
    public class EmployeeValidationTests
    {
        private ValidationService _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ValidationService();
        }

        [Test]
        public void InvalidEmail_ShouldReturnFalse()
        {
            // Arrange
            string email = "john.doe-at-email.com";

            // Act
            bool isValid = _validator.IsValidEmail(email);

            // Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void NegativeSalary_ShouldThrowException()
        {
            // Arrange
            decimal salary = -30000;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => ValidateSalary(salary));
            Assert.That(ex.Message, Is.EqualTo("Salary must be a positive amount."));
        }

        [Test]
        public void ValidData_ShouldNotThrow()
        {
            // Arrange
            string email = "john.doe@email.com";
            decimal salary = 40000;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                if (!_validator.IsValidEmail(email))
                    throw new ArgumentException("Invalid email format.");

                ValidateSalary(salary);
            });
        }
        private void ValidateSalary(decimal salary)
        {
            if (salary < 0)
                throw new ArgumentException("Salary must be a positive amount.");
        }
    }
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
    }
}
