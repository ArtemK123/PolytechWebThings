using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.LoginUser;
using Application.Users.Queries.GetUserByEmail;
using Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UserApiController : ControllerBase
    {
        private readonly ISender mediator;

        public UserApiController(ISender mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand createUserCommand)
        {
            await mediator.Send(createUserCommand);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand loginUserCommand)
        {
            await mediator.Send(loginUserCommand);
            IUser user = await mediator.Send(new GetUserByEmailQuery { Email = loginUserCommand.Email });
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                CreateUserPrincipal(user),
                new AuthenticationProperties());

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetEmail()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.Email);
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