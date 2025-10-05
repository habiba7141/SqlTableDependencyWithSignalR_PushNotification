
using Microsoft.AspNetCore.Builder;
using SignalR_PushNotification.Models;
using SignalR_PushNotification.Services;

namespace SignalR_PushNotification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();
            // ✅ سجل خدمة SQLTableDependency
            builder.Services.AddSingleton<NewsSqlNotificationService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapHub<NewsHub>("/newsHub");
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<NewsSqlNotificationService>();
                service.Start();
            }

            app.Run();
        }
    }
}
