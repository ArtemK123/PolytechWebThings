using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Application;
using Domain;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using PolytechWebThings.Infrastructure;
using Web.Attributes;
using Web.Middlewares;
using Web.Models.OperationResults;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomain();
            services.AddApplication();
            services.AddInfrastructure();
            services.AddWeb();

            services
                .AddControllers(options => options.Filters.Add(new ValidateModelFilter()))
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = false;
                    options.Events.OnRedirectToLogin = async context =>
                    {
                        context.Response.StatusCode = 200;
                        OperationResult operationResult = new OperationResult(OperationStatus.Unauthorized);
                        byte[] serialized = JsonSerializer.SerializeToUtf8Bytes(operationResult);
                        await context.Response.Body.WriteAsync(serialized, 0, serialized.Length);
                    };
                });

            // In production, the Webpack files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            app.UseExceptionHandler("/internal/error");

            if (Configuration.GetValue<bool>("UseDefaultFiles"))
            {
                app.UseDefaultFiles(new DefaultFilesOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/dist"))
                });
            }

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}");
            });

            app.UseNotFoundForApiCall();

            app.UseSpa(spa =>
            {
                spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "serve");
                }
            });

            hostApplicationLifetime.ApplicationStarted.Register(() => ApplicationStartedCallback(app));
        }

        private static void ApplicationStartedCallback(IApplicationBuilder app)
        {
            IEnumerable<IStartupJob> startupJobs = app.ApplicationServices.GetServices<IStartupJob>();
            IEnumerable<IStartupJob> orderedJobs = startupJobs.OrderBy(job => job.Priority);
            foreach (IStartupJob job in orderedJobs)
            {
                job.Execute(app.ApplicationServices);
            }
        }
    }
}