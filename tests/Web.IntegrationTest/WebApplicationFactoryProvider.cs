using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Web.IntegrationTest
{
    internal class WebApplicationFactoryProvider
    {
        public WebApplicationFactory<Startup> GetWebApplicationFactory()
        {
            return new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureTestServices(services =>
                    {
                    });
                });
        }
    }
}