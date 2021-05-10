using System.Security.Claims;
using ControllerHttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace Web.Providers
{
    internal class UserEmailProvider : IUserEmailProvider
    {
        public string GetUserEmail(ControllerHttpContext httpContent)
        {
            return httpContent.User.FindFirstValue(ClaimTypes.Email);
        }
    }
}