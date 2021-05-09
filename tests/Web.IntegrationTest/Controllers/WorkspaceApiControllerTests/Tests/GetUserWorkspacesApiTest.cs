﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.Request;
using Web.Models.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(UserApiController))]
    internal class GetUserWorkspacesApiTest : WorkspaceApiControllerTestBase
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        [Test]
        public async Task GetUserWorkspaces_Unauthorized_ShouldReturnUnauthorizedResponse()
        {
            await UserApiProxy.LogoutAsync();
            HttpResponseMessage response = await SendGetUserWorkspacesRequest();
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetUserWorkspaces_NoWorkspaces_ShouldReturnEmptyCollection()
        {
            HttpResponseMessage response = await SendGetUserWorkspacesRequest();
            string responseText = await response.Content.ReadAsStringAsync();
            GetUserWorkspacesResponse responseData = JsonSerializer.Deserialize<GetUserWorkspacesResponse>(responseText, jsonSerializerOptions) ?? throw new NullReferenceException();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsEmpty(responseData.Workspaces);
        }

        [Test]
        public async Task GetUserWorkspaces_WithWorkspaces_ShouldReturnAllWorkspaces()
        {
            IReadOnlyCollection<CreateWorkspaceRequest> createWorkspaceRequests = new[]
            {
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}1", AccessToken = $"{AccessToken}1", GatewayUrl = $"{GatewayUrl}1" },
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}2", AccessToken = $"{AccessToken}2", GatewayUrl = $"{GatewayUrl}2" },
                new CreateWorkspaceRequest { Name = $"{WorkspaceName}3", AccessToken = $"{AccessToken}3", GatewayUrl = $"{GatewayUrl}3" },
            };

            foreach (CreateWorkspaceRequest createWorkspaceRequest in createWorkspaceRequests)
            {
                GatewayConnectorMock.Setup(connector => connector.CanConnectToGatewayAsync(createWorkspaceRequest.GatewayUrl, createWorkspaceRequest.AccessToken)).ReturnsAsync(true);
                await SendCreateWorkspaceRequest(createWorkspaceRequest);
            }

            HttpResponseMessage response = await SendGetUserWorkspacesRequest();
            string responseText = await response.Content.ReadAsStringAsync();
            GetUserWorkspacesResponse responseData = JsonSerializer.Deserialize<GetUserWorkspacesResponse>(responseText, jsonSerializerOptions) ?? throw new NullReferenceException();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.True(
                responseData
                    .Workspaces.All(responseModel =>
                        createWorkspaceRequests.Any(
                            requestModel => AreSame(requestModel, responseModel))));
            Assert.True(responseData.Workspaces.All(workspace => workspace.Id != default));
        }

        private async Task<HttpResponseMessage> SendGetUserWorkspacesRequest()
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/WorkspaceApi/GetUserWorkspaces"));
        }

        private bool AreSame(CreateWorkspaceRequest requestModel, WorkspaceApiModel responseModel)
            => requestModel.Name == responseModel.Name
               && requestModel.GatewayUrl == responseModel.GatewayUrl;
    }
}