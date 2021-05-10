using FluentValidation.Results;
using NUnit.Framework;
using Web.Validators.Workspace;

namespace Web.UnitTest.Validators.Workspace
{
    internal class IdValidatorTest
    {
        private IdValidator idValidator;

        [SetUp]
        public void SetUp()
        {
            idValidator = new IdValidator();
        }

        [TestCase(0, "Non-positive ids are not supported", TestName = "Should return bad request when workspaceId is 0")]
        [TestCase(-1, "Non-positive ids are not supported", TestName = "Should return bad request when workspaceId is negative number")]
        public void Validate_InvalidModel(int? id, string expectedMessage)
        {
            ValidationResult actual = idValidator.Validate(id);
            Assert.AreEqual(expectedMessage, actual.ToString());
        }

        [TestCase(1)]
        public void Validate_ValidModel(int? id)
        {
            ValidationResult actual = idValidator.Validate(id);
            Assert.IsEmpty(actual.Errors);
        }
    }
}