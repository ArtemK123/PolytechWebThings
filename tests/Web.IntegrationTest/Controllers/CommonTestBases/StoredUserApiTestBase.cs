using System.Threading.Tasks;
using NUnit.Framework;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Controllers.CommonTestBases
{
    internal abstract class StoredUserApiTestBase : WebApiIntegrationTestBase
    {
        private const string UserPassword = "123123";
        private const string UserEmail = "test@gmail.com";
        private const string AnotherUserEmail = "another@test.com";

        protected UserApiClient UserApiClient { get; private set; }

        [SetUp]
        public async Task StoredUserApiTestBaseSetUp()
        {
            UserApiClient = new UserApiClient(HttpClient, new HttpResponseMessageParser());
            await UserApiClient.CreateAsync(new CreateUserRequest { Email = UserEmail, Password = UserPassword });
            await UserApiClient.LoginAsync(new LoginUserRequest { Email = UserEmail, Password = UserPassword });
        }

        protected async Task ChangeUserAsync()
        {
            await UserApiClient.LogoutAsync();
            await UserApiClient.CreateAsync(new CreateUserRequest { Email = AnotherUserEmail, Password = UserPassword });
            await UserApiClient.LoginAsync(new LoginUserRequest { Email = AnotherUserEmail, Password = UserPassword });
        }
    }
}