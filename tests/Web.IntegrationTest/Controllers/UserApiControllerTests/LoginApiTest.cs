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
    internal class LoginApiTest : WebApiIntegrationTestBase
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
        public async Task Login_ValidCredentials_ShouldReturnCookieWithToken()
        {
            HttpResponseMessage response = await userApiClient.LoginRawResponseAsync(new LoginUserRequest { Email = Email, Password = Password });

            string cookieHeaderValue = response.Headers.GetValues("Set-Cookie").SingleOrDefault();
            string[] cookieHeaderElements = cookieHeaderValue?.Split(';').Select(element => element.Trim()).ToArray();
            string token = cookieHeaderElements?[0].Split('=')[1];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.False(string.IsNullOrEmpty(token));
        }

        [Test]
        public async Task Login_InvalidModel_ShouldReturnBadRequestResponse_AndValidationMessage()
        {
            OperationResult response = await userApiClient.LoginAsync(new LoginUserRequest { Email = string.Empty, Password = string.Empty });

            string expectedResponseMessage = "{\"Email\":[\"A valid email address is required.\"],\"Password\":[\"'Password' must not be empty.\"]}";

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual(expectedResponseMessage, response.Message);
        }

        [Test]
        public async Task Login_UserIsNotFound_ShouldReturnBadRequest()
        {
            const string wrongUserEmail = "another@test.com";
            OperationResult response = await userApiClient.LoginAsync(new LoginUserRequest { Email = wrongUserEmail, Password = Password });

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"User with email={wrongUserEmail} is not found", response.Message);
        }

        [Test]
        public async Task Login_WrongPassword_ShouldReturnBadRequest()
        {
            const string wrongPassword = "wrongPassword";
            OperationResult response = await userApiClient.LoginAsync(new LoginUserRequest { Email = Email, Password = wrongPassword });

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Wrong password for user with email={Email}", response.Message);
        }
    }
}