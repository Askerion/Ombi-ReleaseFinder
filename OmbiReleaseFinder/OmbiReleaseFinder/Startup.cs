using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmbiReleaseFinder.Classes;
using OmbiReleaseFinder.Models;

namespace OmbiReleaseFinder
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //SQL Server hinzufügen
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=Database;Trusted_Connection=True;";
            //services.AddDbContext<MovieDatabaseContext>(options => options.UseSqlServer(connection));
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=MovieDatabase;Trusted_Connection=True;";
            //services.AddDbContext<MovieDatabaseContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<MovieDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MovieDatabase")));



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc();

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, FtpScheduleTask>();

            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, MovieSearchScheduleTask>();

            services.Configure<AppSettingFtp>(Configuration.GetSection("FTPSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
