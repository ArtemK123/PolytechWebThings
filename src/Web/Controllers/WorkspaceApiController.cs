using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;

namespace Web.Controllers
{
    public class WorkspaceApiController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task Create([FromBody] CreateWorkspaceRequest createWorkspaceRequest)
        {
            throw new NotImplementedException();
        }
    }
}