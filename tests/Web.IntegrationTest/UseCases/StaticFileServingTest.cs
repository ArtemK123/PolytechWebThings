using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Web.IntegrationTest.UseCases
{
    internal class StaticFileServingTest : WebApiIntegrationTestBase
    {
        [TestCase("")]
        [TestCase("test")]
        [TestCase("login/")]
        [TestCase("page/feature")]
        public async Task NonApiCall_ShouldReturnIndexHtml(string url)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "url"));
            string responseText = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.True(responseText.Contains("<html>"));
        }

        [TestCase("api", HttpStatusCode.NotFound)]
        [TestCase("api/InvalidApi", HttpStatusCode.NotFound)]
        [TestCase("api/InvalidApi/TestAction", HttpStatusCode.NotFound)]
        [TestCase("api/HealthCheckApi/InvalidAction", HttpStatusCode.NotFound)]
        [TestCase("api/HealthCheckApi/HealthCheck", HttpStatusCode.OK)]
        public async Task ApiCall_ShouldReturnNotFoundWhenActionIsNotFound(string url, HttpStatusCode expectedCode)
        {
            HttpResponseMessage response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            string responseText = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(expectedCode, response.StatusCode);
            Assert.False(responseText.Contains("<html>"));
        }
    }
}