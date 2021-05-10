using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands.CreateUser;
using Application.Commands.LoginUser;
using Application.Commands.LogoutUser;
using Application.Queries.GetUserByEmail;
using Domain.Entities.User;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.User.Request;
using Web.Providers;

namespace Web.Controllers
{
    public class UserApiController : ControllerBase
    {
        private readonly ISender mediator;
        private readonly IUserEmailProvider userEmailProvider;

        public UserApiController(ISender mediator, IUserEmailProvider userEmailProvider)
        {
            this.mediator = mediator;
            this.userEmailProvider = userEmailProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest createUserRequest)
        {
            await mediator.Send(request: new CreateUserCommand(
                email: createUserRequest.Email ?? throw new NullReferenceException(),
                password: createUserRequest.Password ?? throw new NullReferenceException(),
                role: UserRole.User));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginUserRequest loginUserRequest)
        {
            await mediator.Send(new LoginUserCommand(
                email: loginUserRequest.Email ?? throw new NullReferenceException(),
                password: loginUserRequest.Password ?? throw new NullReferenceException()));

            IUser user = await mediator.Send(new GetUserByEmailQuery(email: loginUserRequest.Email));
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                CreateUserPrincipal(user),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string userEmail = userEmailProvider.GetUserEmail(HttpContext);
            await mediator.Send(new LogoutUserCommand { Email = userEmail });
            await HttpContext.SignOutAsync();
            return Ok();
        }

        private ClaimsPrincipal CreateUserPrincipal(IUser user)
        {
            IReadOnlyCollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}