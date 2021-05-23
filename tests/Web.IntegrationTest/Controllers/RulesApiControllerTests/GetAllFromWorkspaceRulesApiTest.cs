using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities.Rule;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Utils;
using Web.Models.OperationResults;
using Web.Models.Rules;
using Web.Models.Rules.Request;
using Web.Models.Rules.Response;
using Web.Models.Rules.Steps;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class GetAllFromWorkspaceRulesApiTest : RulesApiTestBase
    {
        private GetAllFromWorkspaceRequest GetRequest => new GetAllFromWorkspaceRequest { WorkspaceId = WorkspaceId };

        [Test]
        public async Task GetAllFromWorkspace_UnauthorizedUser_ShouldReturnUnauthorizedStatus()
        {
            await UserApiClient.LogoutAsync();
            OperationResult<GetAllFromWorkspaceResponse> result = await RulesApiClient.GetAllFromWorkspaceAsync(GetRequest);
            Assert.AreEqual(OperationStatus.Unauthorized, result.Status);
        }

        [Test]
        public async Task GetAllFromWorkspace_InvalidRequestModel_ShouldReturnErrorMessage()
        {
            OperationResult<GetAllFromWorkspaceResponse> result = await RulesApiClient.GetAllFromWorkspaceAsync(GetRequest with { WorkspaceId = -1 });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual("{\"WorkspaceId\":[\"Non-positive ids are not supported\"]}", result.Message);
        }

        [Test]
        public async Task GetAllFromWorkspace_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            int nonExistingWorkspaceId = WorkspaceId + 1;
            OperationResult<GetAllFromWorkspaceResponse> result = await RulesApiClient.GetAllFromWorkspaceAsync(GetRequest with { WorkspaceId = nonExistingWorkspaceId });
            Assert.AreEqual(OperationStatus.Error, result.Status);
            Assert.AreEqual($"Workspace with id={nonExistingWorkspaceId} is not found", result.Message);
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

            OperationResult<GetAllFromWorkspaceResponse> getAllRulesResponse = await RulesApiClient.GetAllFromWorkspaceAsync(GetRequest);

            Assert.AreEqual(OperationStatus.Success, getAllRulesResponse.Status);
            Assert.True(CollectionComparer.Compare(currentWorkspaceRules, getAllRulesResponse.Data.Rules, CompareRules));
            Assert.False(getAllRulesResponse.Data.Rules.Any(rule => CompareRules(otherWorkspaceRule, rule)));
        }

        private async Task CreateRulesAsync(IReadOnlyCollection<CreateRuleRequest> rulesToCreate)
        {
            foreach (CreateRuleRequest createRuleRequest in rulesToCreate)
            {
                OperationResult<CreateRuleResponse> response = await RulesApiClient.CreateAsync(createRuleRequest);
                if (response.Status != OperationStatus.Success)
                {
                    throw new Exception(response.Message);
                }
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
                        new StepApiModel { ExecutionOrderPosition = 1, StepType = StepType.ExecuteRule, RuleName = "RuleA" }
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

        private async Task<int> CreateOtherWorkspaceAsync()
        {
            CreateWorkspaceRequest createOtherWorkspaceCommand = new CreateWorkspaceRequest
            {
                Name = WorkspaceName + "_1",
                AccessToken = AccessToken,
                GatewayUrl = "https://othergateway:1123"
            };

            MockGatewayPingEndpoint(HttpStatusCode.OK, createOtherWorkspaceCommand.GatewayUrl);
            await SetupThingsEndpointMockAsync(createOtherWorkspaceCommand.GatewayUrl);
            await WorkspaceApiClient.CreateAsync(createOtherWorkspaceCommand);
            OperationResult<GetUserWorkspacesResponse> userWorkspacesResponse = await WorkspaceApiClient.GetUserWorkspacesAsync();

            int otherWorkspaceId = userWorkspacesResponse.Data.Workspaces.Single(workspace => workspace.Id != WorkspaceId).Id;
            return otherWorkspaceId;
        }
    }
}