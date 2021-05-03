using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PolytechWebThings.Infrastructure.Database;

namespace Web.IntegrationTest
{
    [TestFixture]
    internal abstract class WebApiIntegrationTestBase
    {
        protected WebApplicationFactory<Startup> ApplicationFactory { get; private set; }

        protected HttpClient HttpClient { get; private set; }

        [SetUp]
        public void WebApiIntegrationTestBaseSetUp()
        {
            ApplicationFactory = new WebApplicationFactoryProvider().GetWebApplicationFactory(SetupMocks);
            HttpClient = ApplicationFactory.CreateClient();
        }

        [TearDown]
        public void WebApiIntegrationTestBaseTearDown()
        {
            ApplicationFactory.Services.GetService<ApplicationDbContext>()?.Database.EnsureDeleted();
            ApplicationFactory.Dispose();
            HttpClient.Dispose();
        }

        protected virtual void SetupMocks(IServiceCollection services)
        {
            MockDatabase(services);
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