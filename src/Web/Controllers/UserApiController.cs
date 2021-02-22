using System.Threading.Tasks;
using Application.Users.Commands.CreateUser;
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
    }
}