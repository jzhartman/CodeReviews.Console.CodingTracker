using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using CodingTracker.Data.TypeHandlers;
using CodingTracker.Models.Services;
using CodingTracker.Services;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Runtime.Serialization;

namespace CodingTracker.ConsoleApp
{
    internal static class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var connectionString = ConfigurationManager.AppSettings.Get("connectionString");
            
            var dateTimeFormat = ConfigurationManager.AppSettings.Get("timeAndDateFormat");
            SqlMapper.AddTypeHandler(new DateTimeHandler(dateTimeFormat));

            var services = new ServiceCollection();
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
