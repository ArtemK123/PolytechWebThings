using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;

namespace Web.IntegrationTest.Controllers.ThingsApiControllerTests
{
    [TestFixture(TestOf = typeof(ThingsApiController))]
    internal class ChangePropertyStateTest : WebApiIntegrationTestBase
    {
        [Test]
        public async Task ChangePropertyState_UnauthorizedUser_ShouldReturnUnauthorizedOperationResult()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_InvalidModel_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetThing_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_CanNotFindTargetProperty_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_InvalidNewValue_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task ChangePropertyState_Success_ShouldReturnSuccessOperationResult()
        {
            throw new NotImplementedException();
        }
    }
}