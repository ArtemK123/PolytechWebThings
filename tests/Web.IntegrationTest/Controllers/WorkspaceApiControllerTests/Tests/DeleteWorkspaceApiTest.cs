using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Controllers;

namespace Web.IntegrationTest.Controllers.WorkspaceApiControllerTests.Tests
{
    [TestFixture(TestOf = typeof(WorkspaceApiController))]
    internal class DeleteWorkspaceApiTest : WorkspaceApiControllerTestBase
    {
        [Test]
        public Task Delete_UnauthorizedUser_ShouldReturnUnauthorizedResponse()
        {
            throw new NotImplementedException();
        }

        [Test]
        public Task Delete_UserDoesNotHaveEnoughRights_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public Task Delete_WorkspaceIsNotFound_ShouldReturnErrorMessage()
        {
            throw new NotImplementedException();
        }

        [Test]
        public Task Delete_Success_ShouldReturnOkResponse()
        {
            throw new NotImplementedException();
        }
    }
}