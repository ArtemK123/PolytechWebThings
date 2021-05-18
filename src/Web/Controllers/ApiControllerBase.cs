using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(ISender mediator)
        {
            Mediator = mediator;
        }

        protected ISender Mediator { get; }

        protected string UserEmail => HttpContext.User.FindFirstValue(ClaimTypes.Email);
    }
}