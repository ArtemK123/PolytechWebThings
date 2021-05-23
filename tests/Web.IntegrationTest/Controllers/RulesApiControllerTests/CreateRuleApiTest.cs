using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class CreateRuleApiTest : RulesApiTestBase
    {
        private const string CreatedRuleName = "CreatedRule";
        private const string OtherRuleName = "OtherRuleName";

        private StepApiModel DefaultExecuteRuleStep => new StepApiModel { ExecutionOrderPosition = 0, StepType = StepType.ExecuteRule, RuleName = CreatedRuleName };

        private StepApiModel DefaultChangePropertyStateStep
            => new StepApiModel { ExecutionOrderPosition = 0, StepType = StepType.ChangeThingState, ThingId = ThingId, PropertyName = PropertyName, NewPropertyState = NewPropertyState };

        private CreateRuleRequest DefaultRequest => new CreateRuleRequest
        {
            WorkspaceId = WorkspaceId,
            RuleName = CreatedRuleName,
            Steps = new[] { DefaultChangePropertyStateStep }
        };

        [Test]
        public async Task Create_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<CreateRuleResponse> result = await RulesApiClient.CreateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task Create_InvalidModel_ShouldReturnErrorMessage()
        {
            await RunCreateWithErrorTestAsync(
                new CreateRuleRequest(),
                "{\"Steps\":[\"'Steps' must not be empty.\"],\"RuleName\":[\"'Rule Name' must not be empty.\"],\"WorkspaceId\":[\"'Workspace Id' must not be empty.\"]}");
        }

        [Test]
        public async Task Create_CanNotFindWorkspace_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            await RunCreateWithErrorTestAsync(
                DefaultRequest with { WorkspaceId = nonExistingWorkspaceId },
                $"Workspace with id={nonExistingWorkspaceId} is not found");
        }

        [Test]
        public async Task Create_RuleNameDuplicated_ShouldReturnErrorMessage()
        {
            await RulesApiClient.CreateAsync(DefaultRequest);
            await RunCreateWithErrorTestAsync(DefaultRequest, $"Rule with name={CreatedRuleName} is already created");
        }

        [Test]
        public async Task Create_ExecuteRuleStep_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            string unknownRuleName = "unknown rule";
            StepApiModel executeUnknownRuleStep = DefaultExecuteRuleStep with { RuleName = unknownRuleName };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(executeUnknownRuleStep), $"Can not find rule with name={unknownRuleName}");
        }

        [Test]
        public async Task Create_ChangePropertyState_ThingIsNotFound_ShouldReturnErrorMessage()
        {
            string invalidThingId = "invalid thing id";
            StepApiModel changeUnknownThingStateStep = DefaultChangePropertyStateStep with { ThingId = invalidThingId };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeUnknownThingStateStep), $"Can not find thing with id={invalidThingId}");
        }

        [Test]
        public async Task Create_ChangePropertyState_PropertyIsNotFound_ShouldReturnErrorMessage()
        {
            string invalidPropertyName = "invalid property name";
            StepApiModel changeUnknownPropertyStateStep = DefaultChangePropertyStateStep with { PropertyName = invalidPropertyName };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeUnknownPropertyStateStep), $"Can not find property with name={invalidPropertyName}");
        }

        [Test]
        public async Task Create_ChangePropertyState_NewValueIsInvalid_ShouldReturnErrorMessage()
        {
            var invalidPropertyState = "invalidValue";
            StepApiModel changeInvalidStateStep = DefaultChangePropertyStateStep with { NewPropertyState = invalidPropertyState };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeInvalidStateStep), $"Invalid value {invalidPropertyState} for property with type {PropertyValueType}");
        }

        [Test]
        public async Task Create_InvalidExecutionOrder_ShouldReturnErrorMessage()
        {
            StepApiModel changeInvalidStateStep = DefaultChangePropertyStateStep with { ExecutionOrderPosition = 111 };
            await RunCreateWithErrorTestAsync(CreateRequestWithStep(changeInvalidStateStep), "Invalid order of step execution");
        }

        [Test]
        public async Task Create_ChangePropertyStep_Success_ShouldReturnCreatedRuleId()
        {
            OperationResult<CreateRuleResponse> result = await RulesApiClient.CreateAsync(DefaultRequest);
            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.AreNotEqual(default(int), result.Data.CreatedRuleId);
        }

        [Test]
        public async Task Create_ExecuteRuleStep_Success_ShouldReturnSuccessResult()
        {
            await RulesApiClient.CreateAsync(DefaultRequest);

            CreateRuleRequest secondRequest = CreateRequestWithStep(DefaultExecuteRuleStep) with
            {
                RuleName = OtherRuleName

            };
            OperationResult<CreateRuleResponse> result = await RulesApiClient.CreateAsync(secondRequest);
            Assert.AreEqual(OperationStatus.Success, result.Status);
        }

        private async Task RunCreateWithErrorTestAsync(CreateRuleRequest request, string expectedErrorMessage)
        {
            OperationResult<CreateRuleResponse> result = await RulesApiClient.CreateAsync(request);
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual(expectedErrorMessage, result.Message);
        }

        private CreateRuleRequest CreateRequestWithStep(StepApiModel step)
            => DefaultRequest with
            {
                Steps = new[] { step }
            };
    }
}