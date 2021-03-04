using Domain.Entities.Common;
using Domain.Entities.User;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<IFactory<NewUserCreationModel, IUser>, UserFactory>();
            services.AddTransient<IFactory<StoredUserCreationModel, IUser>, UserFactory>();
            return services;
        }
    }
}