﻿using Application;
using Application.MozillaGateway.Checkers;
using Application.MozillaGateway.Providers;
using Application.Repositories;
using Domain.Providers;
using Domain.Updaters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PolytechWebThings.Infrastructure.Database;
using PolytechWebThings.Infrastructure.Database.Providers;
using PolytechWebThings.Infrastructure.Database.Rules;
using PolytechWebThings.Infrastructure.Database.StartupJobs;
using PolytechWebThings.Infrastructure.Database.Users;
using PolytechWebThings.Infrastructure.Database.Workspaces;
using PolytechWebThings.Infrastructure.MozillaGateway.Checkers;
using PolytechWebThings.Infrastructure.MozillaGateway.Creators;
using PolytechWebThings.Infrastructure.MozillaGateway.Creators.PropertyCreators;
using PolytechWebThings.Infrastructure.MozillaGateway.Parsers;
using PolytechWebThings.Infrastructure.MozillaGateway.Providers;
using PolytechWebThings.Infrastructure.MozillaGateway.Resolvers;
using PolytechWebThings.Infrastructure.MozillaGateway.Senders;
using PolytechWebThings.Infrastructure.MozillaGateway.Updaters;
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
            services.AddTransient<IGatewayConnectionChecker, GatewayConnectionChecker>();
            services.AddTransient<IThingsProvider, ThingsProvider>();
            services.AddTransient<IGatewayMessageSender, GatewayMessageSender>();
            services.AddTransient<IPropertyParserResolver, PropertyParserResolver>();
            services.AddTransient<IPropertyValueUpdater, PropertyValueUpdater>();
            services.AddTransient<IThingStateProvider, ThingStateProvider>();
            services.AddTransient<IGetThingsResponseParser, GetThingsResponseParser>();
            services.AddTransient<IThingCreator, ThingCreator>();
            services.AddTransient<IPropertyCreator, BooleanPropertyCreator>();
            services.AddTransient<IPropertyCreator, StringPropertyCreator>();
            services.AddTransient<IPropertyCreator, NumberPropertyCreator>();
            services.AddTransient<IPropertyCreator, IntegerPropertyCreator>();
        }

        private static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=Database:ConnectionString"));
            services.AddSingleton<IStartupJob, EnsureDatabaseCreatedJob>();
            services.AddTransient<IDatabaseCreationTypeProvider, DatabaseCreationTypeProvider>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorkspaceRepository, WorkspaceRepository>();
            services.AddTransient<IRuleRepository, RuleRepository>();
        }
    }
}