using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.UserApiControllerTests;
using Web.Models.Request;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    internal class WorkspaceApiControllerTestBase : WebApiIntegrationTestBase
    {
        protected const string WorkspaceName = "TestName";
        protected const string GatewayUrl = "http://localhost:8080";
        protected const string AccessToken = "j.w.t";
        private const string UserEmail = "test@gmail.com";
        private const string UserPassword = "123123";

        protected UserApiProxy UserApiProxy { get; private set; }

        protected Mock<IGatewayConnector> GatewayConnectorMock { get; private set; }

        [SetUp]
        public async Task WorkspaceApiControllerTestBaseSetUp()
        {
            GatewayConnectorMock = new Mock<IGatewayConnector>(MockBehavior.Strict);
            UserApiProxy = new UserApiProxy(HttpClient);
            await UserApiProxy.CreateAsync(new CreateUserRequest { Email = UserEmail, Password = UserPassword });
            await UserApiProxy.LoginAsync(new LoginUserRequest { Email = UserEmail, Password = UserPassword });
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            services.AddTransient(_ => GatewayConnectorMock.Object);
        }

        protected async Task<HttpResponseMessage> SendCreateWorkspaceRequest(CreateWorkspaceRequest requestModel)
        {
            return await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/WorkspaceApi/Create")
            {
                Content = JsonContent.Create(requestModel)
            });
        }
    }
}