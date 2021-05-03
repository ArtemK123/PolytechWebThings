using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Web.IntegrationTest
{
    internal class WebApplicationFactoryProvider
    {
        public WebApplicationFactory<Startup> GetWebApplicationFactory(Action<IServiceCollection> setupMocksAction)
        {
            return new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Test");
                    builder.ConfigureTestServices(setupMocksAction);
                });
        }
    }
}