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

        [SetUp]
        public void SetUp()
        {
            rulesApiClient = new RulesApiClient(HttpClient, new HttpResponseMessageParser());
        }

        [Test]
        public async Task Create_Poc()
        {
            CreateRuleRequest request = new CreateRuleRequest
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

            OperationResult<CreateRuleResponse> result = await rulesApiClient.CreateAsync(request);
            Assert.NotNull(result);
        }
    }
}