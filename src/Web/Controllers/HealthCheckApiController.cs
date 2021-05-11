using Microsoft.AspNetCore.Mvc;
using Web.Models.OperationResults;

namespace Web.Controllers
{
    public class HealthCheckApiController : ControllerBase
    {
        [HttpGet]
        public OperationResult<string> HealthCheck()
        {
            return new OperationResult<string>(OperationStatus.Success, "Hello from backend!");
        }
    }
}