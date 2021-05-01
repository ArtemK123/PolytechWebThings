using System.Collections.Generic;
using System.Linq;
using Application;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolytechWebThings.Infrastructure;

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

            services.AddControllers();

            // In production, the Webpack files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(_ => { });

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