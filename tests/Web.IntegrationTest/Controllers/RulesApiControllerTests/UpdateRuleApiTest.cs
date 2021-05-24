using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Steps;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestOf(typeof(RulesApiController))]
    internal class UpdateRuleApiTest : RulesApiTestBase
    {
        // private UpdateRuleRequest UpdateRequest => new UpdateRuleRequest
        // {
        //     RuleId = RuleId,
        //     NewRuleName = "UpdatedRuleName",
        //     UpdatedSteps = new StepApiModel[]
        //     {
        //
        //     }
        // };

        private UpdateRuleRequest UpdateRequest => throw new NotImplementedException();

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
            throw new NotImplementedException();
        }

        [Test]
        public async Task Update_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Update_StepsAreEmpty_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Update_NewExecuteRuleStep_CircularReference_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Update_Success_ShouldUpdateRules()
        {
            throw new NotImplementedException();
        }
    }
}