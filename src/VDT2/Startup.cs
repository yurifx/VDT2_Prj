using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VDT2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Ref: Strongly Typed Configuration Settings in ASP.NET Core
            // https://weblog.west-wind.com/posts/2016/may/23/strongly-typed-configuration-settings-in-aspnet-core

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<Models.Configuracao>(Configuration.GetSection("ConfiguracaoVDT"));

            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton<IConfiguration>(Configuration);

            // Add framework services.
            services.AddMvc();

            //add the AddSession() and AddDistributedMemoryCache() lines to the ConfigureServices(IServiceCollection services)
            //http://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            // https://docs.asp.net/en/latest/publishing/iis.html
            services.Configure<IISOptions>(options => {
                options.AutomaticAuthentication = false;
            });


            // Nota: A connection string para acessar a base de dados está no arquivo appsettings.json
            // Ref: Connection Strings
            // https://docs.efproject.net/en/latest/miscellaneous/connection-strings.html
            services.AddEntityFrameworkSqlServer()
            .AddDbContext<VDT2.DAL.GeralDbContext>(options =>
            options.UseSqlServer(Configuration.GetSection("ConfiguracaoVDT").GetValue<string>("ConnectionStringVDT")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();


            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = VDT2.BLL.Globais.NomeCookieAutenticacao,
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Index/"),
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Index/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                CookieHttpOnly = true,
                ExpireTimeSpan = new TimeSpan(99999,0,0,0,0),
                //ExpireTimeSpan = new TimeSpan(0, VDT2.BLL.Globais.ExpiracaoCookieAutenticacao, 0),
                SlidingExpiration = true
            });


            app.UseSession();

            app.UseDeveloperExceptionPage();

            app.UseMvc(routes =>
            {
                
                // Working with MVC Areas in Asp.Net vNext (MVC 6)
                // http://timjames.me/blog/2014/12/13/mvc-areas-with-vnext/
                //routes.MapRoute(name: "areaRoute",
                //    template: "{area:exists}/{controller}/{action}",
                //    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Diag.Log.PastaLog = Configuration.GetSection("ConfiguracaoVDT").GetValue<string>("PastaLog");
            Diag.Log.NivelLog = Configuration.GetSection("ConfiguracaoVDT").GetValue<string>("NivelLog");
        }
    }
}
