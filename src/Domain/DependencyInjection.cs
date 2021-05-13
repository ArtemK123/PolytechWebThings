using Domain.Entities.Common;
using Domain.Entities.User;
using Domain.Entities.Workspace;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<IFactory<NewUserCreationModel, IUser>, UserFactory>();
            services.AddTransient<IFactory<StoredUserCreationModel, IUser>, UserFactory>();
            services.AddTransient<IFactory<NewWorkspaceCreationModel, IWorkspace>, WorkspaceFactory>();
            services.AddTransient<IFactory<StoredWorkspaceCreationModel, IWorkspace>, WorkspaceFactory>();
            // services.AddTransient<IFactory<ParsedThingCreationModel, IThing>, ThingFactory>();
            // services.AddTransient<IFactory<ParsedPropertyCreationModel, IProperty>, PropertyFactory>();
            // services.AddTransient<IFactory<ParsedActionCreationModel, IAction>, ActionFactory>();
            return services;
        }
    }
}