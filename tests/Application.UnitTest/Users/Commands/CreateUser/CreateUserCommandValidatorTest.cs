using System.Linq;
using Application.Users.Commands.CreateUser;
using FluentValidation.Results;
using NUnit.Framework;

namespace Application.UnitTest.Users.Commands.CreateUser
{
    internal class CreateUserCommandValidatorTest
    {
        private const string ValidEmail = "client@somewhere.com";
        private const string ValidPassword = "123123";

        private CreateUserCommandValidator createUserCommandValidator;

        [SetUp]
        public void SetUp()
        {
            createUserCommandValidator = new CreateUserCommandValidator();
        }

        [TestCase(null, "'Email' must not be empty.", TestName = "Should fail validation when email is null")]
        [TestCase("", "A valid email address is required.", TestName = "Should fail validation when email is empty string")]
        [TestCase("test.com", "A valid email address is required.", TestName = "Should fail validation when email does not have @ symbol")]
        [TestCase("@.", "A valid email address is required.", TestName = "Should fail validation when email does not have text")]
        [TestCase(ValidEmail, null, TestName = "Should pass validation")]
        public void EmailTest(string input, string expectedValidationMessage)
        {
            var command = new CreateUserCommand { Email = input, Password = ValidPassword };
            ValidationResult actualValidationResult = createUserCommandValidator.Validate(command);
            if (expectedValidationMessage is not null)
            {
                Assert.AreEqual(expectedValidationMessage, actualValidationResult.Errors.SingleOrDefault()?.ErrorMessage);
                return;
            }

            Assert.IsTrue(actualValidationResult.IsValid);
        }

        [TestCase(null, "'Password' must not be empty.", TestName = "Should fail validation when password is null")]
        [TestCase("", "'Password' must not be empty.", TestName = "Should fail validation when password is empty string")]
        [TestCase(ValidPassword, null, TestName = "Should pass validation")]
        public void PasswordTest(string input, string expectedValidationMessage)
        {
            var command = new CreateUserCommand { Email = ValidEmail, Password = input };
            ValidationResult actualValidationResult = createUserCommandValidator.Validate(command);
            if (expectedValidationMessage is not null)
            {
                Assert.AreEqual(expectedValidationMessage, actualValidationResult.Errors.SingleOrDefault()?.ErrorMessage);
                return;
            }

            Assert.IsTrue(actualValidationResult.IsValid);
        }
    }
}