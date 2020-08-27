using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace Spudder
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
            // Set up database

            DBConfig dbConfig = null;

            string dbConfigPath = Directory.GetCurrentDirectory() + "/DBConfig.json";

            if (File.Exists(dbConfigPath))
            {
                string data = File.ReadAllText(dbConfigPath);
                dbConfig = JsonConvert.DeserializeObject<DBConfig>(data);
            }
            else
            {
                dbConfig = new DBConfig()
                {
                    Host = "INSERT HOST",
                    Password = "INSERT PASSWORD",
                    Username = "INSERT USERNAME"
                };

                string data = JsonConvert.SerializeObject(dbConfig);

                File.WriteAllText(dbConfigPath, data);
            }

            DBConfig.instance = dbConfig;

            services.AddDbContextPool<SpudderDB>(options =>
            {
                options.UseMySql(SpudderDB.ConnectionString, options => options.EnableRetryOnFailure().CharSet(CharSet.Utf8Mb4).ServerVersion(new Version(8, 0, 20), ServerType.MySql));
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
