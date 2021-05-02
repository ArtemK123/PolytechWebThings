using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Models.Request;

namespace Web.IntegrationTest.Controllers.UserApiController.Tests
{
    internal class LogoutApiTest : WebApiIntegrationTestBase
    {
        private const string Email = "test@mail.com";
        private const string Password = "123213123";

        private UserApiProxy userApiProxy;

        [SetUp]
        public async Task SetUp()
        {
            userApiProxy = new UserApiProxy(HttpClient);
            await userApiProxy.CreateAsync(new CreateUserRequest { Email = Email, Password = Password });
        }

        [Test]
        public async Task Logout_AuthenticatedUser_ShouldSignOutAuthenticatedUser_AndClearCookie()
        {
            await userApiProxy.LoginAsync(new LoginUserRequest { Email = Email, Password = Password });
            HttpResponseMessage response = await userApiProxy.LogoutAsync();

            string cookieHeaderValue = response.Headers.GetValues("Set-Cookie").SingleOrDefault();
            string[] cookieHeaderElements = cookieHeaderValue?.Split(';').Select(element => element.Trim()).ToArray();
            string token = cookieHeaderElements?[0].Split('=')[1];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(string.Empty, token);
        }

        [Test]
        public async Task Logout_UnauthenticatedUser_ShouldReturnUnauthenticatedResponse()
        {
            HttpResponseMessage response = await userApiProxy.LogoutAsync();

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}