using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    internal class UpdateRuleApiTest : RulesApiTestBase
    {
        private UpdateRuleRequest UpdateRequest => throw new NotImplementedException();

        [Test]
        public async Task Update_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }
    }
}