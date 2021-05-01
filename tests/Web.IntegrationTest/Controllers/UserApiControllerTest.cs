using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users;
using Application.Users.Commands.CreateUser;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Web.IntegrationTest.Controllers
{
    public class UserApiControllerTest
    {
        private WebApplicationFactory<Startup> webApplicationFactory;
        private HttpClient httpClient;

        [SetUp]
        public void SetUp()
        {
            webApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [TearDown]
        public void TearDown()
        {
            webApplicationFactory.Dispose();
            httpClient.Dispose();
        }

        [Test]
        public async Task Create_ValidUser_ShouldCreateUser()
        {
            const string email = "test31232121@gmail.com";
            const string password = "12345678";
            const UserRole role = UserRole.User;

            HttpResponseMessage responseMessage = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = email, Password = password, Role = role })
            });
            IUserRepository userRepository = webApplicationFactory.Services.GetService<IUserRepository>() ?? throw new NullReferenceException();
            IUser storedUser = await userRepository.GetByEmailAsync(email) ?? throw new NullReferenceException();

            Assert.True(responseMessage.IsSuccessStatusCode);
            Assert.AreEqual(email, storedUser.Email);
            Assert.AreEqual(password, storedUser.Password);
            Assert.AreEqual(role, storedUser.Role);
            Assert.True(string.IsNullOrEmpty(storedUser.SessionToken));
            Assert.False(string.IsNullOrEmpty(storedUser.Id));
        }

        [Test]
        public async Task Create_InvalidRequest_ShouldReturnValidationMessages()
        {
            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = string.Empty, Password = string.Empty })
            });

            var responseMessage = await response.Content.ReadAsStringAsync();

            Assert.Fail();
        }
    }
}