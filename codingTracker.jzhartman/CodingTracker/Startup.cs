using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using CodingTracker.Models.Services;
using CodingTracker.Services;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CodingTracker.ConsoleApp
{
    internal static class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var connectionString = ConfigurationManager.AppSettings.Get("connectionString");

            services.AddSingleton<ISqliteConnectionFactory>(provider => new SqliteConnectionFactory(connectionString));
            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
            services.AddSingleton<ICodingSessionRepository, CodingSessionRepository>();


            services.AddSingleton<ICodingSessionService, CodingSessionService>();

            services.AddSingleton<CodingSessionView>();

            services.AddSingleton<MenuController>();


            return services.BuildServiceProvider();
        }

    }
}
