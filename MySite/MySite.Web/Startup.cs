using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySite.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog.Extensions.Logging;

namespace MySite.Web
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

            ///var connection = "Filename=MySiteDemo.db";
            ///services.AddDbContext<DataContext>(options => options.UseSqlite(connection));
            var mysqlconnection = "Server=127.0.0.1;database=madb;uid=root;pwd=1qaz2wsxE;SslMode=None";
            services.AddDbContext<DataContext>(options => options.UseMySql(mysqlconnection));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/Account/IndexCookie/";
                //options.Cookie.Expiration = new TimeSpan(0, 0, 30);
                //options.ReturnUrlParameter = "";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            loggerFactory.AddNLog();
            env.ConfigureNLog("Nlog.config");
            app.UseCookiePolicy();
            //app.UseSession();
            app.UseAuthentication();

            //app.UseCookieAuthentication(options =>
            //{
            //    options.AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.LoginPath = new PathString("/user/login");
            //    options.AutomaticAuthenticate = true;
            //    options.AutomaticChallenge = true;
            //    options.CookieHttpOnly = true;
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Doctor}/{action=Index}/{id?}");
            });
        }
    }
}
