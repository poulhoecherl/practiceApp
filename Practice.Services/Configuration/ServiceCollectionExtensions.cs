using Microsoft.Extensions.DependencyInjection;
using Practice.Services.Interfaces;
using Practice.Services.Services;

namespace Practice.Services.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPracticeServices(this IServiceCollection services)
        {
            //services.AddScoped<IMappingService, MappingService>();
            //services.AddScoped<ISongService, SongService>();
            //services.AddScoped<ISongsService, SongsService>();
            //services.AddScoped<ISessionService, SessionService>();
            // Add more services here if needed
        }
    }
}
