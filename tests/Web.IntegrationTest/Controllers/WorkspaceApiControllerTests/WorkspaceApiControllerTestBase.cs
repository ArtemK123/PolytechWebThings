﻿using System.Threading.Tasks;
using Application.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Web.IntegrationTest.Controllers.UserApiControllerTests;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests
{
    internal abstract class WorkspaceApiControllerTestBase : WebApiIntegrationTestBase
    {
        protected const string WorkspaceName = "TestName";
        protected const string GatewayUrl = "http://localhost:8080";
        protected const string AccessToken = "j.w.t";
        private const string UserPassword = "123123";
        private const string UserEmail = "test@gmail.com";
        private const string AnotherUserEmail = "another@test.com";

        protected UserApiProxy UserApiProxy { get; private set; }

        protected WorkspaceApiClient WorkspaceApiClient { get; private set; }

        protected Mock<IGatewayConnector> GatewayConnectorMock { get; private set; }

        [SetUp]
        public async Task WorkspaceApiControllerTestBaseSetUp()
        {
            GatewayConnectorMock = new Mock<IGatewayConnector>(MockBehavior.Strict);
            UserApiProxy = new UserApiProxy(HttpClient);
            WorkspaceApiClient = new WorkspaceApiClient(HttpClient);
            await UserApiProxy.CreateAsync(new CreateUserRequest { Email = UserEmail, Password = UserPassword });
            await UserApiProxy.LoginAsync(new LoginUserRequest { Email = UserEmail, Password = UserPassword });
        }

        protected override void SetupMocks(IServiceCollection services)
        {
            base.SetupMocks(services);
            services.AddTransient(_ => GatewayConnectorMock.Object);
        }

        protected async Task ChangeUserAsync()
        {
            await UserApiProxy.LogoutAsync();
            await UserApiProxy.CreateAsync(new CreateUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            await UserApiProxy.LoginAsync(new LoginUserRequest { Email = AnotherUserEmail, Password = UserPassword });
        }
    }
}