using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;

namespace Web.Controllers
{
    public class WorkspaceApiController : ControllerBase
    {
        [HttpPost]
        public async Task Create([FromBody] CreateWorkspaceRequest createWorkspaceRequest)
        {
            throw new NotImplementedException();
        }
    }
}