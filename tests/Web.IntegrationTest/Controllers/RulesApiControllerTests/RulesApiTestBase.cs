using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.CommonTestBases.MockedGatewayThingsApi;
using Web.IntegrationTest.Utils;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Rules;
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

        protected bool CompareRules(CreateRuleRequest createRuleRequest, RuleApiModel rule)
            => createRuleRequest.WorkspaceId == rule.WorkspaceId
               && createRuleRequest.RuleName == rule.Name
               && CollectionComparer.Compare(createRuleRequest.Steps, rule.Steps, CompareSteps);

        protected bool CompareSteps(StepApiModel stepA, StepApiModel stepB)
            => stepA.StepType == stepB.StepType
               && stepA.ExecutionOrderPosition == stepB.ExecutionOrderPosition
               && stepA.RuleName == stepB.RuleName
               && stepA.ThingId == stepB.ThingId
               && stepA.PropertyName == stepB.PropertyName
               && stepA.NewPropertyState == stepB.NewPropertyState;
    }
}