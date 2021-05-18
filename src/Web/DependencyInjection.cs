using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}