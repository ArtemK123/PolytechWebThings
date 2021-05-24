using System;
using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
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
            Assert.AreEqual("replace with message", result.Message);
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
            int nonExistingRuleId = RuleId + 100;
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest with { RuleId = nonExistingRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("replace with message", result.Message);
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
        public async Task Update_Success_ShouldUpdateRules()
        {
            OperationResult result = await RulesApiClient.UpdateAsync(UpdateRequest);
            Assert.AreEqual(OperationStatus.Success, result.Status);
            throw new NotImplementedException();
            // TODO: Implement GetRuleById endpoint and use there in order to check updated rule
        }
    }
}