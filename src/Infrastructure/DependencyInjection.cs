using Application.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Database;
using PolytechWebThings.Infrastructure.Database.Users;

namespace PolytechWebThings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}