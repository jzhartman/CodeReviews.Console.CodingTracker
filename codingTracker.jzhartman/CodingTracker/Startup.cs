using CodingTracker.Controller;
using CodingTracker.Controller.Interfaces;
using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using CodingTracker.Data.Repositories;
using CodingTracker.Data.TypeHandlers;
using CodingTracker.Services;
using CodingTracker.Services.Interfaces;
using CodingTracker.Views;
using CodingTracker.Views.Interfaces;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CodingTracker.ConsoleApp;
internal static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var connectionString = ConfigurationManager.AppSettings.Get("connectionString");
        
        var dateTimeFormat = ConfigurationManager.AppSettings.Get("timeAndDateFormat");
        SqlMapper.RemoveTypeMap(typeof(DateTime));
        SqlMapper.RemoveTypeMap(typeof(DateTime?));
        SqlMapper.AddTypeHandler(new DateTimeHandler(dateTimeFormat));

        var services = new ServiceCollection();

        //Register All Controllers
        services.AddSingleton<IMainMenuController, MainMenuController>();
        services.AddSingleton<ITrackSessionController, TrackSessionController>();
        services.AddSingleton<IEntryListController, EntryListController>();
        services.AddSingleton<IReportsController, ReportsController>();
        services.AddSingleton<IGoalsController, GoalsController>();

        //Register All Services
        services.AddSingleton<ICodingSessionDataService, CodingSessionDataService>();

        //Resgister All Views
        services.AddSingleton<IMenuView, MenuView>();
        services.AddSingleton<IConsoleOutputView, ConsoleOutputView>();
        services.AddSingleton<IUserInputView>(new UserInputView(dateTimeFormat));


        services.AddSingleton<ICodingSessionRepository, CodingSessionRepository>();
        services.AddSingleton<ISqliteConnectionFactory>(provider => new SqliteConnectionFactory(connectionString));
        services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();

        return services.BuildServiceProvider();
    }

}
