﻿using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.LoginUser;
using NUnit.Framework;

namespace Web.IntegrationTest.UseCases
{
    internal class UserApiTests : WebApiIntegrationTestBase
    {
        [Test]
        [Ignore("Will be refactored later")] // TODO: Refactor or delete
        public async Task RegisterAndLoginTest()
        {
            const string email = "test31232121@gmail.com";
            const string password = "12345678";

            await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Create")
            {
                Content = JsonContent.Create(new CreateUserCommand { Email = email, Password = password })
            });

            HttpResponseMessage loginResponse = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/UserApi/Login")
            {
                Content = JsonContent.Create(new LoginUserCommand { Email = email, Password = password })
            });

            Assert.True(loginResponse.IsSuccessStatusCode);

            var cookieHeaderValue = loginResponse.Headers.GetValues("Set-Cookie").Single();
            var cookieHeaderElements = cookieHeaderValue.Split(';').Select(element => element.Trim()).ToArray();
            var token = cookieHeaderElements[0].Split('=')[1];
            HttpResponseMessage getRestrictedResourceResponse = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/UserApi/GetEmail")
            {
                Headers =
                {
                    { "Cookie", $".AspNetCore.Cookies={token}" }
                }
            });
            Assert.True(getRestrictedResourceResponse.IsSuccessStatusCode);
        }
    }
}