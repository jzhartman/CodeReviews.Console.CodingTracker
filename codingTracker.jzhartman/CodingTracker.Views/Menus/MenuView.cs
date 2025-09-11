using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Views.Interfaces;
using Spectre.Console;

namespace CodingTracker.Views.Menus
{
    public class MenuView : IMenuView
    {
        public string RenderMainMenuAndGetSelection()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select from the options below:")
                    .AddChoices(new[]
                    {
                        "Track Session",
                        "View/Manage Entries",
                        "View Reports",
                        "Manage Goal",
                        "Exit"
                    })
                );

            return selection;
        }

        public string RenderTrackingMenuAndGetSelection()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("How would you like to track your coding session?")
                    .AddChoices(new[]
                    {
                        "Enter Start and End Times",
                        "Begin Timer",
                        "Return to Main Menu"
                    })
                );

            return selection;
        }

        public string RenderEntryViewOptionsAndGetSelection()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("How would you like your entries displayed?")
                    .AddChoices(new[]
                    {
                        "All",
                        "One Year",
                        "Year to Date",
                        "Enter Date Range",
                        "Return to Previous Menu"
                    })
                );
            return selection;
        }
        public string RenderUpdateOrDeleteOptionsAndGetSelection()
        {
            var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select the next operation:")
                .AddChoices(new[]
                {
                                "Change Record",
                                "Delete Record",
                                "Return to Previous Menu"
                })
            );
            return selection;
        }

        public string RenderUpdateTimeFeildSelector()
        {
            var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option to change:")
                .AddChoices(new[]
                {
                                "Start Time",
                                "End Time",
                })
            );
            return selection;
        }

    }
}
