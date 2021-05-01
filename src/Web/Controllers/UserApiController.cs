using System;
using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.LoginUser;
using Application.Users.Queries.GetUserByEmail;
using Domain.Entities.User;
using MediatR;
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
        public async Task<string> Login([FromBody]LoginUserCommand loginUserCommand)
        {
            await mediator.Send(loginUserCommand);
            IUser user = await mediator.Send(new GetUserByEmailQuery { Email = loginUserCommand.Email });
            return user.SessionToken ?? throw new Exception("User token is null in the inappropriate context");
        }
    }
}