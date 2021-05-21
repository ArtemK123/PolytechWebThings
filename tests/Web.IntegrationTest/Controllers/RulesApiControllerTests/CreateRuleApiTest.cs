using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Controllers.CommonTestBases;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Rules;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class CreateRuleApiTest : StoredWorkspaceApiTestBase
    {
        private const string PropertyName = "property1";
        private const string NewPropertyState = "123";
        private const string CreatedRuleName = "CreatedRule";
        private static readonly string ThingId = GatewayUrl + "/things/thing-1";

        private RulesApiClient rulesApiClient;

        private StepApiModel DefaultExecuteRuleStep => new StepApiModel { StepType = StepType.ExecuteRule, RuleName = CreatedRuleName };

        private StepApiModel DefaultChangePropertyStateStep
            => new StepApiModel { StepType = StepType.ChangeThingState, ThingId = ThingId, PropertyName = PropertyName, NewPropertyState = NewPropertyState };

        private CreateRuleRequest DefaultRequest => new CreateRuleRequest
        {
            WorkspaceId = WorkspaceId,
            RuleCreationModel = new RuleCreationApiModel
            {
                RuleName = CreatedRuleName,
                Steps = new[] { DefaultChangePropertyStateStep }
            }
        };

        [SetUp]
        public void SetUp()
        {
            rulesApiClient = new RulesApiClient(HttpClient, new HttpResponseMessageParser());
        }

        [Test]
        public async Task Create_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<CreateRuleResponse> result = await rulesApiClient.CreateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task Create_InvalidModel_ShouldReturnErrorMessage()
        {
            await RunCreateWithErrorTestAsync(new CreateRuleRequest(), "Replace with actual message");
        }

        [Test]
        public async Task Create_CanNotFindWorkspace_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            await RunCreateWithErrorTestAsync(DefaultRequest with { WorkspaceId = nonExistingWorkspaceId }, "Replace with actual message");
        }

        [Test]
        public async Task Create_RuleNameDuplicated_ShouldReturnErrorMessage()
        {
            await rulesApiClient.CreateAsync(DefaultRequest);
            await RunCreateWithErrorTestAsync(DefaultRequest, "Replace with actual message");
        }

        [Test]
        public async Task Create_ExecuteRuleStep_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            StepApiModel executeUnknownRuleStep = DefaultExecuteRuleStep with { RuleName = "unknown rule" };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(executeUnknownRuleStep), "Replace with actual message");
        }

        [Test]
        public async Task Create_ChangePropertyState_ThingIsNotFound_ShouldReturnErrorMessage()
        {
            StepApiModel changeUnknownThingStateStep = DefaultChangePropertyStateStep with { ThingId = "invalid thing id" };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeUnknownThingStateStep), "Replace with actual message");
        }

        [Test]
        public async Task Create_ChangePropertyState_PropertyIsNotFound_ShouldReturnErrorMessage()
        {
            StepApiModel changeUnknownPropertyStateStep = DefaultChangePropertyStateStep with { PropertyName = "invalid property name" };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeUnknownPropertyStateStep), "Replace with actual message");
        }

        [Test]
        public async Task Create_ChangePropertyState_NewValueIsInvalid_ShouldReturnErrorMessage()
        {
            StepApiModel changeInvalidStateStep = DefaultChangePropertyStateStep with { NewPropertyState = null };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeInvalidStateStep), "Replace with actual message");
        }

        [Test]
        public async Task Create_ChangePropertyStep_Success_ShouldReturnCreatedRuleId()
        {
            OperationResult<CreateRuleResponse> result = await rulesApiClient.CreateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreNotEqual(default(int), result.Data.CreatedRuleId);
        }

        [Test]
        public async Task Create_ExecuteRuleStep_Success_ShouldReturnSuccessResult()
        {
            await rulesApiClient.CreateAsync(DefaultRequest);
            OperationResult<CreateRuleResponse> result = await rulesApiClient.CreateAsync(CreateRequestWithStep(DefaultExecuteRuleStep));
            Assert.AreEqual(OperationStatus.Success, result.Status);
        }

        private async Task RunCreateWithErrorTestAsync(CreateRuleRequest request, string expectedErrorMessage)
        {
            OperationResult<CreateRuleResponse> result = await rulesApiClient.CreateAsync(request);
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual(expectedErrorMessage, result.Message);
        }

        private CreateRuleRequest CreateRequestWithStep(StepApiModel step)
            => DefaultRequest with
            {
                RuleCreationModel = DefaultRequest.RuleCreationModel with
                {
                    Steps = new[] { step }
                }
            };
    }
}