using Microsoft.EntityFrameworkCore;
using PolytechWebThings.Infrastructure.Database.Users;

namespace PolytechWebThings.Infrastructure.Database
{
    internal sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDatabaseModel> Users { get; init; } = null!;
    }
}