using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    internal abstract class WorkspaceApiControllerTestBase : WebApiIntegrationTestBase
    {
        protected const string WorkspaceName = "TestName";
        protected const string GatewayUrl = "http://localhost:1214";
        protected const string AccessToken = "j.w.t";
        private const string UserPassword = "123123";
        private const string UserEmail = "test@gmail.com";
        private const string AnotherUserEmail = "another@test.com";

        protected UserApiClient UserApiClient { get; private set; }

        protected WorkspaceApiClient WorkspaceApiClient { get; private set; }

        protected Mock<HttpMessageHandler> HttpMessageHandlerMock { get; private set; }

        [SetUp]
        public async Task WorkspaceApiControllerTestBaseSetUp()
        {
            UserApiClient = new UserApiClient(HttpClient, new HttpResponseMessageParser());
            WorkspaceApiClient = new WorkspaceApiClient(HttpClient, new HttpResponseMessageParser());
            await UserApiClient.CreateAsync(new CreateUserRequest { Email = UserEmail, Password = UserPassword });
            await UserApiClient.LoginAsync(new LoginUserRequest { Email = UserEmail, Password = UserPassword });
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);

            HttpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            MockHttpClientFactory(services: services);
        }

        protected void MockGatewayConnectionCheck(bool validConnection, string gatewayUrl = GatewayUrl, string accessToken = AccessToken)
        {
            HttpStatusCode responseStatus = validConnection ? HttpStatusCode.OK : HttpStatusCode.Unauthorized;
            SetupHttpMessageHandlerMock(
                request
                    => request.RequestUri?.AbsoluteUri == gatewayUrl + "/ping"
                       && request.Method == HttpMethod.Get
                       && request.Headers.Authorization?.Scheme == "Bearer"
                       && request.Headers.Authorization?.Parameter == accessToken
                       && request.Headers.Accept.Single().MediaType == "application/json",
                new HttpResponseMessage(responseStatus));
        }

        protected void SetupHttpMessageHandlerMock(Func<HttpRequestMessage, bool> isRequestValidFunc, HttpResponseMessage response)
        {
            HttpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(message => isRequestValidFunc(message)), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

        protected async Task ChangeUserAsync()
        {
            await UserApiClient.LogoutAsync();
            await UserApiClient.CreateAsync(new CreateUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            await UserApiClient.LoginAsync(new LoginUserRequest { Email = AnotherUserEmail, Password = UserPassword });
        }

        private void MockHttpClientFactory(IServiceCollection services)
        {
            Mock<IHttpClientFactory> httpClientFactoryMock = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            HttpClient httpClient = new HttpClient(HttpMessageHandlerMock.Object);
            httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);
            services.AddTransient(_ => httpClientFactoryMock.Object);
        }
    }
}