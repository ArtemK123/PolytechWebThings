using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HealthCheckApiController : ControllerBase
    {
        [HttpGet]
        public string HealthCheck()
        {
            return "Hello from backend!";
        }
    }
}