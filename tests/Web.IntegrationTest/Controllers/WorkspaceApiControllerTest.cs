using System;
using NUnit.Framework;

namespace Web.IntegrationTest.Controllers
{
    public class WorkspaceApiControllerTest : WebApiIntegrationTestBase
    {
        [Test]
        public void Create_InvalidModel_ShouldReturnBadRequest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Create_GatewayUrlIsAlreadyUsed_ShouldReturnBadRequest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Create_CannotConnectToGateway_ShouldReturnBadRequest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Create_Success_ShouldReturnOkResult()
        {
            throw new NotImplementedException();
        }
    }
}