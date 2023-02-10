//using SmartBreadcrumbs.Extensions;
//using System.Reflection;
//using Synotech.SMP.Core.Services.User;
//using Synotech.SMP.Core.Interfaces.User;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
//{
//    options.TagName = "nav";
//    options.TagClasses = "";
//    options.OlClasses = "breadcrumb";
//    options.LiClasses = "breadcrumb-item";
//    options.ActiveLiClasses = "breadcrumb-item active";
//});

//builder.Services.AddScoped<IUserService, UserService>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}





//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();



//app.UseAuthorization();

////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapAreaControllerRoute(
//       name: "Portal",
//       areaName: "Portal",
//       pattern: "Portal/{controller=Home}/{action=Index}/{id?}"

//   );

//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );

//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}"
//    );
//});

//app.Run();


using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Synotech.SMP.Infrastructure.Data;
using Synotech.SMP.Web;

namespace Synotech.SMP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }


        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}