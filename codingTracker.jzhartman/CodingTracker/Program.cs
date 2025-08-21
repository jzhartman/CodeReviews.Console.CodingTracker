using CodingTracker.Console;
using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Configuration;

namespace CodingTracker;
internal class Program
{
    static void Main(string[] args)
    {
        //var connectionString = ConfigurationManager.AppSettings.Get("connectionString");
        //var connectionFactory = new SqliteConnectionFactory(connectionString);

        // Create a view and pass in the connection factory?
        // var view = new CodingSessionView(connectionString);
        // view.Run();

        var serviceProvider = Startup.ConfigureServices();
        var dbInitializer = serviceProvider.GetRequiredService<IDatabaseInitializer>();
        dbInitializer.Initialize();


    }
}
