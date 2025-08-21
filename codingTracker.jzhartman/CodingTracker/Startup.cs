using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CodingTracker.Console
{
    internal static class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var connectionString = ConfigurationManager.AppSettings.Get("connectionString");

            services.AddSingleton<ISqliteConnectionFactory>(provider => new SqliteConnectionFactory(connectionString));
            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();

            services.AddTransient<ICodingSessionRepository, CodingSessionRepository>();

            return services.BuildServiceProvider();
        }

    }
}
