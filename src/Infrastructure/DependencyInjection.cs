using Application;
using Application.Users;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Database;
using PolytechWebThings.Infrastructure.Database.StartupJobs;
using PolytechWebThings.Infrastructure.Database.Users;
using PolytechWebThings.Infrastructure.Services;

namespace PolytechWebThings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=Database:ConnectionString"));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGuidProvider, GuidProvider>();
            services.AddSingleton<IStartupJob, EnsureDatabaseCreatedJob>();
            services.AddSingleton<IStartupJob, RunMigrationsJob>();

            return services;
        }
    }
}