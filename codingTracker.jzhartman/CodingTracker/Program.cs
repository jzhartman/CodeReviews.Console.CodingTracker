using System;
using CodingTracker.ConsoleApp;
using SQLitePCL;
using CodingTracker.Data;
using CodingTracker.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Configuration;
using CodingTracker.Controller.Interfaces;
using CodingTracker.Models.Services;

namespace CodingTracker.ConsoleApp;
internal class Program
{
    static void Main(string[] args)
    {
        Batteries.Init();
        var timeAndDateFormat = ConfigurationManager.AppSettings.Get("timeAndDateFormat");
        var serviceProvider = Startup.ConfigureServices();
        serviceProvider.GetRequiredService<IDatabaseInitializer>().Initialize();

        serviceProvider.GetRequiredService<MenuController>().Run();
    }
}
