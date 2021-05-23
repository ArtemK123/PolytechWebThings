using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Controllers.CommonTestBases;
using Web.IntegrationTest.Utils;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.Rules;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests.GetAllFromWorkspace
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class GetAllFromWorkspaceRulesApiTest : StoredWorkspaceApiTestBase
    {
        private const string PropertyName = "on";
        private const string NewPropertyState = "false";
        private static readonly string ThingId = GatewayUrl + "/things/virtual-things-0";

        private RulesApiClient rulesApiClient;

        private GetAllFromWorkspaceRequest GetRequest => new GetAllFromWorkspaceRequest { WorkspaceId = WorkspaceId };

        private CreateRuleRequest CreateRuleRequest => new CreateRuleRequest
        {
            RuleName = "Rule1",
            WorkspaceId = WorkspaceId,
            Steps = new[] { new StepApiModel { StepType = StepType.ChangeThingState, ThingId = ThingId, PropertyName = PropertyName, NewPropertyState = NewPropertyState } }
        };

        [SetUp]
        public async Task SetUp()
        {
            rulesApiClient = new RulesApiClient(HttpClient, new HttpResponseMessageParser());
            await MockGatewayThingsEndpointAsync();
            await rulesApiClient.CreateAsync(CreateRuleRequest);
        }

        [Test]
        public async Task GetAllFromWorkspace_UnauthorizedUser_ShouldReturnUnauthorizedStatus()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<GetAllFromWorkspaceResponse> result = await rulesApiClient.GetAllFromWorkspaceAsync(GetRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task GetAllFromWorkspace_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            OperationResult<GetAllFromWorkspaceResponse> result = await rulesApiClient.GetAllFromWorkspaceAsync(GetRequest with { WorkspaceId = -1 });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace with error message", result.Message);
        }

        [Test]
        public async Task GetAllFromWorkspace_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            OperationResult<GetAllFromWorkspaceResponse> result = await rulesApiClient.GetAllFromWorkspaceAsync(GetRequest with { WorkspaceId = WorkspaceId + 1 });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("Replace with error message", result.Message);
        }

        [Test]
        public async Task GetAllFromWorkspace_Success_ShouldReturnStoredRules()
        {
            int otherWorkspaceId = await CreateOtherWorkspaceAsync();
            IReadOnlyCollection<CreateRuleRequest> rulesToCreate = GenerateRuleCreationRequests(otherWorkspaceId: otherWorkspaceId);
            await CreateRulesAsync(rulesToCreate: rulesToCreate);

            IReadOnlyCollection<CreateRuleRequest> currentWorkspaceRules =
                rulesToCreate
                    .Where(createRequest => createRequest.WorkspaceId == WorkspaceId)
                    .Union(new[] { CreateRuleRequest })
                    .ToArray();
            CreateRuleRequest otherWorkspaceRule = rulesToCreate.Single(createRequest => createRequest.WorkspaceId == otherWorkspaceId);

            OperationResult<GetAllFromWorkspaceResponse> getAllRulesResponse = await rulesApiClient.GetAllFromWorkspaceAsync(GetRequest);

            Assert.AreEqual(OperationStatus.Success, getAllRulesResponse.Status);
            Assert.True(CollectionComparer.Compare(currentWorkspaceRules, getAllRulesResponse.Data.Rules, CompareRules));
            Assert.False(getAllRulesResponse.Data.Rules?.Any(rule => CompareRules(otherWorkspaceRule, rule)));
        }

        private async Task CreateRulesAsync(IReadOnlyCollection<CreateRuleRequest> rulesToCreate)
        {
            foreach (CreateRuleRequest createRuleRequest in rulesToCreate)
            {
                await rulesApiClient.CreateAsync(createRuleRequest);
            }
        }

        private IReadOnlyCollection<CreateRuleRequest> GenerateRuleCreationRequests(int otherWorkspaceId)
        {
            IReadOnlyCollection<CreateRuleRequest> rulesToCreate = new[]
            {
                CreateRuleRequest with { RuleName = "RuleA" },
                CreateRuleRequest with
                {
                    RuleName = "RuleB",
                    Steps = new[]
                    {
                        CreateRuleRequest?.Steps?.First(),
                        new StepApiModel { StepType = StepType.ExecuteRule, RuleName = "RuleA" }
                    }
                },
                CreateRuleRequest with { WorkspaceId = otherWorkspaceId }
            };
            return rulesToCreate;
        }

        private bool CompareRules(CreateRuleRequest createRuleRequest, RuleApiModel rule)
        {
            return createRuleRequest.WorkspaceId == rule.WorkspaceId
                   && createRuleRequest.RuleName == rule.Name
                   && CollectionComparer.Compare(createRuleRequest.Steps, rule.Steps, CompareSteps);
        }

        private bool CompareSteps(StepApiModel stepA, StepApiModel stepB)
            => stepA.StepType == stepB.StepType
               && stepA.ExecutionOrderPosition == stepB.ExecutionOrderPosition
               && stepA.RuleName == stepB.RuleName
               && stepA.ThingId == stepB.ThingId
               && stepA.PropertyName == stepB.PropertyName
               && stepA.NewPropertyState == stepB.NewPropertyState;

        private async Task MockGatewayThingsEndpointAsync()
        {
            string input = await GetSerializedThingsAsync();

            HttpResponseMessage response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(input)
            };

            SetupHttpMessageHandlerMock(IsGatewayThingsEndpointRequest, response);
        }

        private async Task<string> GetSerializedThingsAsync() => await ReadContentFromDiskAsync("Controllers/RulesApiControllerTests/GetAllFromWorkspace/things.json");

        private bool IsGatewayThingsEndpointRequest(HttpRequestMessage httpRequestMessage) => CheckHttpRequestMessage(httpRequestMessage, GatewayUrl + "/things", HttpMethod.Get);

        private async Task<int> CreateOtherWorkspaceAsync()
        {
            CreateWorkspaceRequest createOtherWorkspaceCommand = new CreateWorkspaceRequest
            {
                Name = WorkspaceName + "_1",
                AccessToken = AccessToken,
                GatewayUrl = "https://othergateway:1123"
            };

            MockGatewayPingEndpoint(HttpStatusCode.OK, createOtherWorkspaceCommand.GatewayUrl);
            await WorkspaceApiClient.CreateAsync(createOtherWorkspaceCommand);
            OperationResult<GetUserWorkspacesResponse> userWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesAsync();

            int otherWorkspaceId = userWorkspacesResponse.Data.Workspaces.Single(workspace => workspace.Id != WorkspaceId).Id;
            return otherWorkspaceId;
        }
    }
}