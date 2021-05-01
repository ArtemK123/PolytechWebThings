using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users;
using Application.Users.Commands.CreateUser;
using Domain.Entities.User;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Web.IntegrationTest.Controllers
{
    internal class UserApiControllerTest : WebApiIntegrationTestBase
    {
        [Test]
        public async Task Create_ValidUser_ShouldCreateUser()
        {
            const string email = "test3213@gmail.com";
            const string password = "12345678";
            const UserRole role = UserRole.User;

            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = email, Password = password, Role = role })
            });
            IUserRepository userRepository = WebApplicationFactory.Services.GetService<IUserRepository>() ?? throw new NullReferenceException();
            IUser storedUser = await userRepository.GetByEmailAsync(email) ?? throw new NullReferenceException();

            Assert.True(response.IsSuccessStatusCode);
            Assert.AreEqual(email, storedUser.Email);
            Assert.AreEqual(password, storedUser.Password);
            Assert.AreEqual(role, storedUser.Role);
            Assert.True(string.IsNullOrEmpty(storedUser.SessionToken));
            Assert.False(string.IsNullOrEmpty(storedUser.Id));
        }

        [Test]
        public async Task Create_InvalidRequest_ShouldReturnValidationMessages()
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = string.Empty, Password = string.Empty })
            });

            string expectedResponseMessage = "Validation failed: " + Environment.NewLine +
                                             " -- Email: Email is required." + Environment.NewLine +
                                             " -- Password: Password is required.";

            string actualResponseMessage = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(expectedResponseMessage, actualResponseMessage);
        }

        [Test]
        public async Task Create_EmailTaken_ShouldReturnBadRequestWithMessage()
        {
            const string email = "test31232121@gmail.com";
            const string password = "12345678";
            const UserRole role = UserRole.User;

            await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = email, Password = password, Role = role })
            });

            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = email, Password = password, Role = role })
            });

            string actualResponseMessage = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(new EmailTakenByOtherUserException(email).Message, actualResponseMessage);
        }
    }
}