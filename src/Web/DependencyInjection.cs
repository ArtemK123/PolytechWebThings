using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Web.Converters;

namespace Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IThingApiModelConverter, ThingApiModelConverter>();
            return services;
        }
    }
}