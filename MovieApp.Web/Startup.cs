using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Data;
using Microsoft.Extensions.Configuration;

namespace MovieApp.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MovieContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MsSQLConnection")));
            //options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews()
                .AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled=true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataSeeding.Seed(app);
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

                //endpoints.MapControllerRoute(
                //    name: "home",
                //    pattern: "",
                //    defaults: new { controller = "Home", Action = "Index" }
                //);

                //endpoints.MapControllerRoute(
                //    name: "about",
                //    pattern: "hakkimizda",
                //    defaults: new { controller = "Home", Action = "About" }
                //);

                //endpoints.MapControllerRoute(
                //    name: "movieList",
                //    pattern: "movies/list",
                //    defaults: new { controller = "Movies", Action = "List"}
                //);

                //endpoints.MapControllerRoute(
                //    name: "movieDetails",
                //    pattern: "movies/details",
                //    defaults: new { controller = "Movies", Action = "Details" }
                //);





            });
        }
    }
}
