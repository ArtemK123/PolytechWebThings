using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Rules;
using Web.Models.Rules.Request;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestOf(typeof(RulesApiController))]
    internal class GetRuleByIdApiTest : RulesApiTestBase
    {
        private GetRuleByIdRequest GetRuleRequest => new GetRuleByIdRequest { RuleId = RuleId };

        [Test]
        public async Task GetById_UnauthorizedUser_ShouldReturnUnauthorizedResult()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<RuleApiModel> result = await RulesApiClient.GetByIdAsync(GetRuleRequest);
            Assert.AreEqual(OperationStatus.Forbidden, result.Status);
        }

        [Test]
        public async Task GetById_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            OperationResult<RuleApiModel> result = await RulesApiClient.GetByIdAsync(new GetRuleByIdRequest());
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace", result.Message);
        }

        [Test]
        public async Task GetById_UserHasNotEnoughRights_ShouldReturnForbiddenResult()
        {
            await ChangeUserAsync();
            OperationResult<RuleApiModel> result = await RulesApiClient.GetByIdAsync(GetRuleRequest);
            Assert.AreEqual(OperationStatus.Forbidden, result.Status);
        }

        [Test]
        public async Task GetById_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingRuleId = RuleId + 10;
            OperationResult<RuleApiModel> result = await RulesApiClient.GetByIdAsync(GetRuleRequest with { RuleId = nonExistingRuleId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace", result.Message);
        }

        [Test]
        public async Task GetById_Success_ShouldReturnRuleApiModel()
        {
            OperationResult<RuleApiModel> result = await RulesApiClient.GetByIdAsync(GetRuleRequest);
            Assert.AreEqual(OperationStatus.Success, result.Status);
            Assert.True(CompareRules(CreateRuleRequest, result.Data));
        }
    }
}