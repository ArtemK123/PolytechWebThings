using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Users;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Web.Models.Request;

namespace Web.IntegrationTest.Controllers.UserApiController.Tests
{
    internal class CreateUserApiTest : WebApiIntegrationTestBase
    {
        private UserApiProxy userApiProxy;

        [SetUp]
        public void SetUp()
        {
            userApiProxy = new UserApiProxy(HttpClient);
        }

        [Test]
        public async Task Create_ValidUser_ShouldCreateUser()
        {
            const string email = "test3213@gmail.com";
            const string password = "12345678";

            HttpResponseMessage response = await userApiProxy.CreateAsync(new CreateUserRequest { Email = email, Password = password });
            IUserRepository userRepository = WebApplicationFactory.Services.GetService<IUserRepository>() ?? throw new NullReferenceException();
            IUser storedUser = await userRepository.GetByEmailAsync(email) ?? throw new NullReferenceException();

            Assert.True(response.IsSuccessStatusCode);
            Assert.AreEqual(email, storedUser.Email);
            Assert.AreEqual(password, storedUser.Password);
            Assert.AreEqual(UserRole.User, storedUser.Role);
            Assert.True(string.IsNullOrEmpty(storedUser.SessionToken));
            Assert.False(string.IsNullOrEmpty(storedUser.Id));
        }

        [Test]
        public async Task Create_InvalidRequest_ShouldReturnValidationMessages()
        {
            HttpResponseMessage response = await userApiProxy.CreateAsync(new CreateUserRequest { Email = string.Empty, Password = string.Empty });

            string expectedResponseMessage = "{\"Email\":[\"A valid email address is required.\"],\"Password\":[\"'Password' must not be empty.\"]}";

            string actualResponseMessage = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedResponseMessage, actualResponseMessage);
        }

        [Test]
        public async Task Create_EmailTaken_ShouldReturnBadRequestWithMessage()
        {
            const string email = "test31232121@gmail.com";
            const string password = "12345678";
            CreateUserRequest createUserRequest = new CreateUserRequest { Email = email, Password = password };

            await userApiProxy.CreateAsync(createUserRequest);

            HttpResponseMessage response = await userApiProxy.CreateAsync(createUserRequest);

            string actualResponseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"Email {email} is already taken by other user", actualResponseMessage);
        }
    }
}