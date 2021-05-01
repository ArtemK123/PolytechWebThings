using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Web.IntegrationTest
{
    [TestFixture]
    public abstract class WebApiIntegrationTestBase
    {
        protected WebApplicationFactory<Startup> WebApplicationFactory { get; private set; }

        protected HttpClient HttpClient { get; private set; }

        [SetUp]
        public void WebApiIntegrationTestBaseSetUp()
        {
            WebApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory();
            HttpClient = WebApplicationFactory.CreateClient();
        }

        [TearDown]
        public void WebApiIntegrationTestBaseTearDown()
        {
            WebApplicationFactory.Dispose();
            HttpClient.Dispose();
        }
    }
}