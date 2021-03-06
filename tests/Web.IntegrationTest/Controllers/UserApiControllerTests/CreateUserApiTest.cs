﻿using System;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Utils.ApiClients;
using Web.IntegrationTest.Utils.Parsers;
using Web.Models.OperationResults;
using Web.Models.User.Request;

namespace Web.IntegrationTest.Controllers.UserApiControllerTests
{
    [TestFixture(TestOf = typeof(UserApiController))]
    internal class CreateUserApiTest : WebApiIntegrationTestBase
    {
        private UserApiClient userApiClient;

        [SetUp]
        public void SetUp()
        {
            userApiClient = new UserApiClient(HttpClient, new HttpResponseMessageParser());
        }

        [Test]
        public async Task Create_ValidUser_ShouldCreateUser()
        {
            const string email = "test3213@gmail.com";
            const string password = "12345678";

            OperationResult response = await userApiClient.CreateAsync(new CreateUserRequest { Email = email, Password = password });
            IUserRepository userRepository = ApplicationFactory.Services.GetService<IUserRepository>() ?? throw new NullReferenceException();
            IUser storedUser = await userRepository.GetByEmailAsync(email) ?? throw new NullReferenceException();

            Assert.AreEqual(OperationStatus.Success, response.Status);
            Assert.AreEqual(email, storedUser.Email);
            Assert.AreEqual(password, storedUser.Password);
            Assert.AreEqual(UserRole.User, storedUser.Role);
            Assert.True(string.IsNullOrEmpty(storedUser.SessionToken));
            Assert.False(string.IsNullOrEmpty(storedUser.Id));
        }

        [Test]
        public async Task Create_InvalidRequest_ShouldReturnValidationMessages()
        {
            OperationResult response = await userApiClient.CreateAsync(new CreateUserRequest { Email = string.Empty, Password = string.Empty });

            string expectedResponseMessage = "{\"Email\":[\"A valid email address is required.\"],\"Password\":[\"'Password' must not be empty.\"]}";

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual(expectedResponseMessage, response.Message);
        }

        [Test]
        public async Task Create_EmailTaken_ShouldReturnBadRequestWithMessage()
        {
            const string email = "test31232121@gmail.com";
            const string password = "12345678";
            CreateUserRequest createUserRequest = new CreateUserRequest { Email = email, Password = password };

            await userApiClient.CreateAsync(createUserRequest);

            OperationResult response = await userApiClient.CreateAsync(createUserRequest);

            Assert.AreEqual(OperationStatus.Error, response.Status);
            Assert.AreEqual($"Email {email} is already taken by other user", response.Message);
        }
    }
}