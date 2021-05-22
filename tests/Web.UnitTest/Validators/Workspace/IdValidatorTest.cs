using FluentValidation.Results;
using NUnit.Framework;
using Web.Validators.Workspace;

namespace Web.UnitTest.Validators.Workspace
{
    internal class IdValidatorTest
    {
        private IntIdValidator intIdValidator;

        [SetUp]
        public void SetUp()
        {
            intIdValidator = new IntIdValidator();
        }

        [TestCase(0, "Non-positive ids are not supported", TestName = "Should return bad request when workspaceId is 0")]
        [TestCase(-1, "Non-positive ids are not supported", TestName = "Should return bad request when workspaceId is negative number")]
        public void Validate_InvalidModel(int? id, string expectedMessage)
        {
            ValidationResult actual = intIdValidator.Validate(id);
            Assert.AreEqual(expectedMessage, actual.ToString());
        }

        [TestCase(1)]
        public void Validate_ValidModel(int? id)
        {
            ValidationResult actual = intIdValidator.Validate(id);
            Assert.IsEmpty(actual.Errors);
        }
    }
}