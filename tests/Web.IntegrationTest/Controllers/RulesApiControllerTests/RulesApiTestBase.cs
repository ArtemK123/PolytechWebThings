using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.CommonTestBases.MockedGatewayThingsApi;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    internal abstract class RulesApiTestBase : MockedGatewayThingsApiTestBase
    {
        protected RulesApiClient RulesApiClient { get; private set; }

        protected int RuleId { get; private set; }

        protected CreateRuleRequest CreateRuleRequest => new CreateRuleRequest
        {
            RuleName = "Rule1",
            WorkspaceId = WorkspaceId,
            Steps = new[]
            {
                new StepApiModel { ExecutionOrderPosition = 0, StepType = StepType.ChangeThingState, ThingId = ThingId, PropertyName = PropertyName, NewPropertyState = NewPropertyState }
            }
        };

        [SetUp]
        public async Task RulesApiTestBaseSetUp()
        {
            RulesApiClient = new RulesApiClient(HttpClient, new HttpResponseMessageParser());
            OperationResult<CreateRuleResponse> result = await RulesApiClient.CreateAsync(CreateRuleRequest);
            RuleId = result.Data.CreatedRuleId;
        }
    }
}