using System;
using NUnit.Framework;
using Web.Controllers;
using Web.IntegrationTest.Controllers.CommonTestBases;

namespace Web.IntegrationTest.Controllers.RulesApiControllerTests
{
    [TestFixture(TestOf = typeof(RulesApiController))]
    internal class CreateRuleApiTest : StoredWorkspaceApiTestBase
    {
        [Test]
        public void Create_Poc()
        {
            throw new NotImplementedException();
        }
    }
}