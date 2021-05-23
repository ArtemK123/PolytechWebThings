using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Controllers.CommonTestBases.MockedGatewayThingsApi;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class DeleteRuleApiTest : MockedGatewayThingsApiTestBase
    {
        private RulesApiClient rulesApiClient;

        private int ruleId;

        private DeleteRuleRequest DeleteRequest => new DeleteRuleRequest { RuleId = ruleId };

        private CreateRuleRequest CreateRuleRequest => new CreateRuleRequest
        {
            RuleName = "Rule1",
            WorkspaceId = WorkspaceId,
            Steps = new[]
            {
                new StepApiModel { ExecutionOrderPosition = 0, StepType = StepType.ChangeThingState, ThingId = ThingId, PropertyName = PropertyName, NewPropertyState = NewPropertyState }
            }
        };

        [SetUp]
        public async Task SetUp()
        {
            rulesApiClient = new RulesApiClient(HttpClient, new HttpResponseMessageParser());
            OperationResult<CreateRuleResponse> result = await rulesApiClient.CreateAsync(CreateRuleRequest);
            ruleId = result.Data.CreatedRuleId;
        }

        [Test]
        public async Task Delete_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult result = await rulesApiClient.DeleteAsync(DeleteRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task Delete_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            int invalidRuleId = -1;
            OperationResult result = await rulesApiClient.DeleteAsync(new DeleteRuleRequest { RuleId = invalidRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace with actual message", result.Message);
        }

        [Test]
        public async Task Delete_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingRuleId = ruleId + 1;
            OperationResult result = await rulesApiClient.DeleteAsync(new DeleteRuleRequest { RuleId = nonExistingRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace with actual message", result.Message);
        }

        [Test]
        public async Task Delete_UserHasNotEnoughRights_ShouldReturnForbiddenResult()
        {
            await ChangeUserAsync();
            OperationResult result = await rulesApiClient.DeleteAsync(DeleteRequest);
            Assert.AreEqual(OperationStatus.Forbidden, result.Status);
        }

        [Test]
        public async Task Delete_Success_ShouldDeleteRule()
        {
            OperationResult deleteResult = await rulesApiClient.DeleteAsync(DeleteRequest);

            OperationResult<GetAllFromWorkspaceResponse> getRulesResult = await rulesApiClient.GetAllFromWorkspaceAsync(new GetAllFromWorkspaceRequest { WorkspaceId = WorkspaceId });

            Assert.AreEqual(OperationStatus.Success, deleteResult.Status);
            Assert.IsEmpty(getRulesResult.Data.Rules);
        }
    }
}