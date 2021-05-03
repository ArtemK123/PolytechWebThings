using Application;
using Application.Connectors;
using Application.Repositories;
using Domain.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Connectors;
using PolytechWebThings.Infrastructure.Database;
using PolytechWebThings.Infrastructure.Database.StartupJobs;
using PolytechWebThings.Infrastructure.Database.Users;
using PolytechWebThings.Infrastructure.Database.Workspaces;
using PolytechWebThings.Infrastructure.Providers;

namespace PolytechWebThings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=Database:ConnectionString"));

            services.AddHttpClient();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorkspaceRepository, WorkspaceRepository>();
            services.AddTransient<IGuidProvider, GuidProvider>();
            services.AddSingleton<IStartupJob, EnsureDatabaseCreatedJob>();
            services.AddTransient<IGatewayConnector, GatewayConnector>();

            return services;
        }
    }
}