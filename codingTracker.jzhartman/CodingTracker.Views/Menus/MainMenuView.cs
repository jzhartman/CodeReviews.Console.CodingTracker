using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CodingTracker.Views.Menus
{
    public static class MainMenuView
    {
        public static string RenderMainMenuAndGetSelection()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select from the options below:")
                    .AddChoices(new[]
                    {
                        "Track Coding",
                        "Manage Entries",
                        "View Reports",
                        "Manage Goal",
                        "Exit"
                    })
                );

            return selection;
        }



        // Move methods below to a different class soon.... Separate out the menus and their display logic?

        public static void RenderTrackMenu()
        {
            var mainMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("How would you like to track your coding session?")
                    .AddChoices(new[]
                    {
                                    "Enter Start and End Times",
                                    "Begin Timer",
                                    "Return to Main Menu"
                    })
                );
        }
        public static void RenderMenu()
        {
            var mainMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("How would you like to track your coding session?")
                    .AddChoices(new[]
                    {
                                    "Enter Start and End Times",
                                    "Begin Timer",
                                    "Return to Main Menu"
                    })
                );
        }
    }
}
