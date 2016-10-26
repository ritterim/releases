using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RimDev.Releases.Infrastructure;
using RimDev.Releases.Infrastructure.GitHub;
using RimDev.Releases.Models;

namespace Site
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(s =>
            {
                var appSettings = new AppSettings();
                Configuration.GetSection("AppSettings").Bind(appSettings);
                return appSettings;
            });

            services.AddSingleton(s =>
            {
                var appEnv = s.GetService<IHostingEnvironment>();
                var settings = s.GetService<AppSettings>();

                var markdownCache = new SqliteMarkdownCache(
                    Path.Combine(
                        appEnv.ContentRootPath,
                        "releases-db.sqlite"));

                return new Client(
                    settings.AccessToken,
                    markdownCache,
                    s.GetService<ILogger<Client>>(),
                    string.IsNullOrEmpty(settings.Company) ? Client.DefaultUserAgent : settings.Company
                );
            });

            services.AddLogging();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Releases}/{action=Index}");
            });
        }
    }
}
