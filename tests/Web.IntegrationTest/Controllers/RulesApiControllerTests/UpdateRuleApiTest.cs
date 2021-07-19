using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Rules;
using Web.Models.Rules.Request;
using Web.Models.Rules.Steps;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestOf(typeof(RulesApiController))]
    internal class UpdateRuleApiTest : RulesApiTestBase
    {
        private CreateRuleRequest ReferencingRule => new CreateRuleRequest
        {
            RuleName = "OtherName",
            WorkspaceId = WorkspaceId,
            Steps = new[] { new StepApiModel { ExecutionOrderPosition = 0, StepType = StepType.ExecuteRule, RuleName = CreateRuleRequest.RuleName } }
        };

        private UpdateRuleRequest UpdateRequest => new UpdateRuleRequest
        {
            RuleId = RuleId,
            NewRuleName = "UpdatedRuleName",
            UpdatedSteps = CreateRuleRequest.Steps
        };

        [SetUp]
        public async Task SetUp()
        {
            await RulesApiClient.CreateAsync(ReferencingRule);
        }

        [Test]
        public async Task Update_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task Update_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            OperationResult result = await RulesApiClient.UpdateAsync(new UpdateRuleRequest());
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("replace with message", result.Message);
        }

        [Test]
        public async Task Update_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingRuleId = RuleId + 100;
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest with { RuleId = nonExistingRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Rule with id=101 is not found", result.Message);
        }

        [Test]
        public async Task Update_UserDoesNotHaveRequiredAccessRights_ShouldReturnForbiddenResult()
        {
            await ChangeUserAsync();
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest);
            Assert.AreEqual(OperationStatus.Forbidden, result.Status);
        }

        [Test]
        public async Task Update_StepsAreEmpty_ShouldReturnErrorMessage()
        {
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest with { UpdatedSteps = Array.Empty<StepApiModel>() });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Steps collection should not be empty. Add at least one step", result.Message);
        }

        [Test]
        public async Task Update_NewExecuteRuleStep_CircularReference_ShouldReturnErrorMessage()
        {
            StepApiModel circularReferenceStep = new StepApiModel { ExecutionOrderPosition = 0, StepType = StepType.ExecuteRule, RuleName = ReferencingRule.RuleName };
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest with { UpdatedSteps = new[] { circularReferenceStep } });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace with actual message", result.Message);
        }

        [Test]
        public async Task Update_NewStepNameIsTaken_ShouldReturnErrorMessage()
        {
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest with { NewRuleName = ReferencingRule.RuleName });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Rule with name={ReferencingRule.RuleName} already exists in workspace with id={WorkspaceId}", result.Message);
        }

        [Test]
        public async Task Update_WrongStepOrder_ShouldReturnErrorMessage()
        {
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest with { UpdatedSteps = UpdateRequest.UpdatedSteps!.Concat(UpdateRequest.UpdatedSteps).ToList() });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Step execution order is broken.", result.Message);
        }

        [Test]
        public async Task Update_Success_ShouldUpdateRules()
        {
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest);
            OperationResult<RuleApiModel> getUpdatedRuleResponse = await RulesApiClient.GetByIdAsync(new GetRuleByIdRequest { RuleId = RuleId });
            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.True(CompareRules(ConvertUpdateRequestToCreateRequest(UpdateRequest), getUpdatedRuleResponse.Data));
        }

        private CreateRuleRequest ConvertUpdateRequestToCreateRequest(UpdateRuleRequest updateRuleRequest)
            => new CreateRuleRequest
            {
                RuleName = updateRuleRequest.NewRuleName, WorkspaceId = WorkspaceId, Steps = updateRuleRequest.UpdatedSteps
            };
    }
}