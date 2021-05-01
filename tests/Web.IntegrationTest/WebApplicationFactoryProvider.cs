using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Database;

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
                    builder.ConfigureTestServices(MockDatabase);
                });
        }

        private static void MockDatabase(IServiceCollection services)
        {
            ServiceDescriptor dbContextOptionsDescriptor = services.SingleOrDefault(
                descriptor => descriptor.ServiceType ==
                              typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(dbContextOptionsDescriptor);

            services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });
        }
    }
}