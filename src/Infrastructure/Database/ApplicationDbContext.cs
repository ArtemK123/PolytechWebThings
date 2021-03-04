﻿using Microsoft.EntityFrameworkCore;
using PolytechWebThings.Infrastructure.Database.Users;

namespace PolytechWebThings.Infrastructure.Database
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserDatabaseModel> Users { get; set; } = null!;
    }
}