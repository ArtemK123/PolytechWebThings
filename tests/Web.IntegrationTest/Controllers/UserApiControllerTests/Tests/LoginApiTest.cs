using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;
using Web.Models.Request;

namespace Web.IntegrationTest.Controllers.UserApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(UserApiController))]
    internal class LoginApiTest : WebApiIntegrationTestBase
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
        public async Task Login_ValidCredentials_ShouldReturnCookieWithToken()
        {
            HttpResponseMessage response = await userApiProxy.LoginAsync(new LoginUserRequest { Email = Email, Password = Password });

            string cookieHeaderValue = response.Headers.GetValues("Set-Cookie").SingleOrDefault();
            string[] cookieHeaderElements = cookieHeaderValue?.Split(';').Select(element => element.Trim()).ToArray();
            string token = cookieHeaderElements?[0].Split('=')[1];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Test]
        public async Task Login_InvalidModel_ShouldReturnBadRequestResponse_AndValidationMessage()
        {
            HttpResponseMessage response = await userApiProxy.LoginAsync(new LoginUserRequest { Email = string.Empty, Password = string.Empty });

            string expectedResponseMessage = "{\"Email\":[\"A valid email address is required.\"],\"Password\":[\"'Password' must not be empty.\"]}";
            string actualResponseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedResponseMessage, actualResponseMessage);
        }

        [Test]
        public async Task Login_UserIsNotFound_ShouldReturnBadRequest()
        {
            const string wrongUserEmail = "another@test.com";
            HttpResponseMessage response = await userApiProxy.LoginAsync(new LoginUserRequest { Email = wrongUserEmail, Password = Password });

            string responseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"User with email={wrongUserEmail} is not found", responseMessage);
        }

        [Test]
        public async Task Login_WrongPassword_ShouldReturnBadRequest()
        {
            const string wrongPassword = "wrongPassword";
            HttpResponseMessage response = await userApiProxy.LoginAsync(new LoginUserRequest { Email = Email, Password = wrongPassword });

            string responseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Wrong password for user with email={Email}", responseMessage);
        }
    }
}