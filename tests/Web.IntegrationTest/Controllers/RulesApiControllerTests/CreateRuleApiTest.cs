using System;
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
        private RulesApiClient rulesApiClient;

        private CreateRuleRequest DefaultRequest => new CreateRuleRequest
        {
            WorkspaceId = WorkspaceId,
            RuleCreationModel = new RuleCreationApiModel
            {
                RuleName = "TestRuleName",
                Steps = new[]
                {
                    new StepApiModel { StepType = StepType.ExecuteRule, RuleName = "Rule to execute 1" },
                    new StepApiModel { StepType = StepType.ChangeThingState, ThingId = "Thing1", PropertyName = "Property1", NewPropertyState = "123" },
                }
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
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_InvalidModel_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_CanNotFindWorkspace_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_RuleNameDuplicated_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_BrokenStepOrder_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_ExecuteRuleStep_RuleIsNotFound_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_ExecuteRuleStep_CircularReference_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_ChangePropertyState_CanNotConnectToGateway_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_ChangePropertyState_ThingIsNotFound_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_ChangePropertyState_PropertyIsNotFound_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_ChangePropertyState_NewValueIsInvalid_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public async Task Create_Success_ShouldReturnNewRuleId()
        {
            throw new NotImplementedException();
        }
    }
}