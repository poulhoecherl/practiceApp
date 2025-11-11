using Autofac.Core;
using Practice.Services.Interfaces;
using Practice.Services.Services;
using Practice.Web.Components;
using Microsoft.EntityFrameworkCore;
using Practice.Data;

namespace Practice.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();



            builder.Services.AddScoped<Practice.Data.Interfaces.IUnitOfWork>();

            builder.Services.AddTransient<ISessionService, SessionService>();

            builder.Services.AddDbContextFactory<PracticeDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
