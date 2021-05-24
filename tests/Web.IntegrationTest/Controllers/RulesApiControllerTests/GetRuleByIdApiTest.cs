using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestOf(typeof(RulesApiController))]
    internal class GetRuleByIdApiTest : RulesApiTestBase
    {
        [Test]
        public async Task GetById_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task GetById_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task GetById_UserHasNotEnoughRights_ShouldReturnForbiddenResult()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task GetById_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task GetById_Success_ShouldReturnRuleApiModel()
        {
            throw new NotImplementedException();
        }
    }
}