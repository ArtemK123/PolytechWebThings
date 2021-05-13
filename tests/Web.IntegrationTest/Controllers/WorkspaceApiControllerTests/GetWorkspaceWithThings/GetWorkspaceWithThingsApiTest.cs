﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using PolytechWebThings.Infrastructure.MozillaGateway.Providers;
using Web.Controllers;
using Web.Models.OperationResults;
using Web.Models.Workspace.Request;
using Web.Models.Workspace.Response;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.GetWorkspaceWithThings
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class GetWorkspaceWithThingsApiTest : WorkspaceApiControllerWithStoredWorkspaceTestBase
    {
        private Mock<HttpMessageHandler> httpMessageHandlerMock;

        [Test]
        public async Task GetWorkspaceWithThings_PrimitivePropertyValues()
        {
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'name':thecodebuzz,'city':'USA'}"),
                });

            OperationResult<GetWorkspaceWithThingsResponse> result = await WorkspaceApiClient.GetWorkspaceWithThingsAsync(new GetWorkspaceWithThingsRequest { WorkspaceId = WorkspaceId });
            Assert.AreEqual(OperationStatus.Success, result.Message);
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);

            httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            httpClientFactoryMock.Setup(factory => factory.CreateClient(nameof(ThingsProvider))).Returns(httpClient);
            services.AddTransient(_ => httpClientFactoryMock.Object);
        }
    }
}