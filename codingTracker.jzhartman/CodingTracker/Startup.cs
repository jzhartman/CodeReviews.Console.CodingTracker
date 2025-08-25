using CodingTracker.Controller;
using CodingTracker.Controller.Interfaces;
using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using CodingTracker.Data.TypeHandlers;
using CodingTracker.Services;
using CodingTracker.Services.Interfaces;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

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

            //Register All Controllers
            services.AddSingleton<IMenuController, MenuController>();

            //Register All Services
            services.AddSingleton<ICodingSessionService, CodingSessionService>();


            services.AddSingleton<ICodingSessionRepository, CodingSessionRepository>();
            services.AddSingleton<ISqliteConnectionFactory>(provider => new SqliteConnectionFactory(connectionString));
            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();

            return services.BuildServiceProvider();
        }

    }
}
