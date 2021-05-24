using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class DeleteRuleApiTest : RulesApiTestBase
    {
        private DeleteRuleRequest DeleteRequest => new DeleteRuleRequest { RuleId = RuleId };

        [Test]
        public async Task Delete_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult result = await RulesApiClient.DeleteAsync(DeleteRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task Delete_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            int invalidRuleId = -1;
            OperationResult result = await RulesApiClient.DeleteAsync(new DeleteRuleRequest { RuleId = invalidRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("{\"RuleId\":[\"Non-positive ids are not supported\"]}", result.Message);
        }

        [Test]
        public async Task Delete_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingRuleId = RuleId + 1;
            OperationResult result = await RulesApiClient.DeleteAsync(new DeleteRuleRequest { RuleId = nonExistingRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Rule with id={nonExistingRuleId} is not found", result.Message);
        }

        [Test]
        public async Task Delete_UserHasNotEnoughRights_ShouldReturnForbiddenResult()
        {
            await ChangeUserAsync();
            OperationResult result = await RulesApiClient.DeleteAsync(DeleteRequest);
            Assert.AreEqual(OperationStatus.Forbidden, result.Status);
        }

        [Test]
        public async Task Delete_Success_ShouldDeleteRule()
        {
            OperationResult deleteResult = await RulesApiClient.DeleteAsync(DeleteRequest);

            OperationResult<GetAllFromWorkspaceResponse> getRulesResult = await RulesApiClient.GetAllFromWorkspaceAsync(new GetAllFromWorkspaceRequest { WorkspaceId = WorkspaceId });

            Assert.AreEqual(OperationStatus.Success, deleteResult.Status);
            Assert.IsEmpty(getRulesResult.Data.Rules);
        }
    }
}