using Application;
using Application.MozillaGateway.Connectors;
using Application.MozillaGateway.Providers;
using Application.Repositories;
using Domain.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Database;
using PolytechWebThings.Infrastructure.Database.Providers;
using PolytechWebThings.Infrastructure.Database.StartupJobs;
using PolytechWebThings.Infrastructure.Database.Users;
using PolytechWebThings.Infrastructure.Database.Workspaces;
using PolytechWebThings.Infrastructure.MozillaGateway.Connectors;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers.PropertyParsers;
using PolytechWebThings.Infrastructure.MozillaGateway.Providers;
using PolytechWebThings.Infrastructure.MozillaGateway.Resolvers;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;
using PolytechWebThings.Infrastructure.Providers;

namespace PolytechWebThings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDatabase();
            services.AddHttpClient();
            services.AddMozillaGateway();
            services.AddTransient<IGuidProvider, GuidProvider>();

            return services;
        }

        private static void AddMozillaGateway(this IServiceCollection services)
        {
            services.AddTransient<IGatewayConnector, GatewayConnector>();
            services.AddTransient<IThingsProvider, ThingsProvider>();
            services.AddTransient<IGatewayMessageSender, GatewayMessageSender>();
            services.AddTransient<IPropertyParserResolver, PropertyParserResolver>();
            services.AddParsers();
        }

        private static void AddParsers(this IServiceCollection services)
        {
            services.AddTransient<IGetThingsResponseParser, GetThingsResponseParser>();
            services.AddTransient<IThingParser, ThingParser>();
            services.AddTransient<IPropertyParser, BooleanPropertyParser>();
            services.AddTransient<IPropertyParser, StringPropertyParser>();
            services.AddTransient<IPropertyParser, NumberPropertyParser>();
            services.AddTransient<IPropertyParser, IntegerPropertyParser>();
        }

        private static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=Database:ConnectionString"));
            services.AddSingleton<IStartupJob, EnsureDatabaseCreatedJob>();
            services.AddTransient<IDatabaseCreationTypeProvider, DatabaseCreationTypeProvider>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorkspaceRepository, WorkspaceRepository>();
        }
    }
}