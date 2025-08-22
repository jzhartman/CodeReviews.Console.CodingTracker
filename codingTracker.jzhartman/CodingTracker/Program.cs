using CodingTracker.Console;
using SQLitePCL;
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
        Batteries.Init();
        var serviceProvider = Startup.ConfigureServices();
        serviceProvider.GetRequiredService<IDatabaseInitializer>().Initialize();

    }
}
