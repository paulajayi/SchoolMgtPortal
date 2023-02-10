using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Extensions;
using Synotech.SMP.Core.Interfaces.User;
using Synotech.SMP.Core.Services.User;
using Synotech.SMP.Infrastructure.Data;
using System.Reflection;

namespace Synotech.SMP.Web
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
            {
                options.TagName = "nav";
                options.TagClasses = "";
                options.OlClasses = "breadcrumb";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
              //  app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                   name: "Portal",
                   areaName: "Portal",
                   pattern: "Portal/{controller=Home}/{action=Index}/{id?}"

               );

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });



        }
    }
}
