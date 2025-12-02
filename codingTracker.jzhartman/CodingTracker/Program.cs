using CodingTracker.Controller.Interfaces;
using CodingTracker.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;

namespace CodingTracker.ConsoleApp;
internal class Program
{
    static void Main(string[] args)
    {
        Batteries.Init();
        var serviceProvider = Startup.ConfigureServices();
        serviceProvider.GetRequiredService<IDatabaseInitializer>().Run();

        serviceProvider.GetRequiredService<IMainMenuController>().Run();
    }
}
