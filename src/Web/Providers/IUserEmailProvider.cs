using ControllerHttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace Web.Providers
{
    public interface IUserEmailProvider
    {
        string GetUserEmail(ControllerHttpContext httpContent);
    }
}