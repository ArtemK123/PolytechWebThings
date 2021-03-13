using System;
using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PolytechWebThings.Infrastructure.Database.StartupJobs
{
    internal class RunMigrationsJob : IStartupJob
    {
        public uint Priority => 2;

        public void Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }
    }
}