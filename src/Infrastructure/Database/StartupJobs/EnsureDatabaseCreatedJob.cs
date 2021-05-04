using System;
using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Database.Enums;
using PolytechWebThings.Infrastructure.Database.Providers;

namespace PolytechWebThings.Infrastructure.Database.StartupJobs
{
    internal class EnsureDatabaseCreatedJob : IStartupJob
    {
        private readonly IDatabaseCreationTypeProvider databaseCreationTypeProvider;

        public EnsureDatabaseCreatedJob(IDatabaseCreationTypeProvider databaseCreationTypeProvider)
        {
            this.databaseCreationTypeProvider = databaseCreationTypeProvider;
        }

        public uint Priority => 1;

        public void Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DatabaseCreationType databaseCreationType = databaseCreationTypeProvider.GetDatabaseCreationType();

            if (databaseCreationType == DatabaseCreationType.DatabaseOnly)
            {
                applicationDbContext.Database.EnsureCreated();
            }
            else if (databaseCreationType == DatabaseCreationType.WithMigrations)
            {
                applicationDbContext.Database.Migrate();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}