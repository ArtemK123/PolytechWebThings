using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Controllers.UserApiControllerTests
{
    [TestFixture(TestOf = typeof(UserApiController))]
    internal class LogoutApiTest : WebApiIntegrationTestBase
    {
        private const string Email = "test@mail.com";
        private const string Password = "123213123";

        private UserApiClient userApiClient;

        [SetUp]
        public async Task SetUp()
        {
            userApiClient = new UserApiClient(HttpClient, new HttpResponseMessageParser());
            await userApiClient.CreateAsync(new CreateUserRequest { Email = Email, Password = Password });
        }

        [Test]
        public async Task Logout_AuthenticatedUser_ShouldSignOutAuthenticatedUser_AndClearCookie()
        {
            await userApiClient.LoginAsync(new LoginUserRequest { Email = Email, Password = Password });
            HttpResponseMessage response = await userApiClient.LogoutRawResponseAsync();

            string cookieHeaderValue = response.Headers.GetValues("Set-Cookie").SingleOrDefault();
            string[] cookieHeaderElements = cookieHeaderValue?.Split(';').Select(element => element.Trim()).ToArray();
            string token = cookieHeaderElements?[0].Split('=')[1];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(string.Empty, token);
        }

        [Test]
        public async Task Logout_UnauthenticatedUser_ShouldReturnUnauthenticatedResponse()
        {
            OperationResult response = await userApiClient.LogoutAsync();

            Assert.AreEqual(OperationStatus.Unauthorized, response.Status);
        }
    }
}