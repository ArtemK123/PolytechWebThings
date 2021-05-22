using Microsoft.EntityFrameworkCore;
using PolytechWebThings.Infrastructure.Database.Rules;
using PolytechWebThings.Infrastructure.Database.Users;
using PolytechWebThings.Infrastructure.Database.Workspaces;

namespace PolytechWebThings.Infrastructure.Database
{
    internal sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDatabaseModel> Users { get; init; } = null!;

        public DbSet<WorkspaceDatabaseModel> Workspaces { get; init; } = null!;

        public DbSet<RuleDatabaseModel> Rules { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDatabaseModel>()
                .HasIndex(user => user.Email)
                .IsUnique();

            builder.Entity<WorkspaceDatabaseModel>()
                .HasIndex(workspace => workspace.GatewayUrl)
                .IsUnique();
        }
    }
}