using CodingTracker.Views.Interfaces;
using Spectre.Console;

namespace CodingTracker.Views;
public class MenuView : IMenuView
{
    public string PrintMainMenuAndGetSelection()
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

    public string PrintTrackingMenuAndGetSelection()
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

    public string PrintEntryViewOptionsAndGetSelection()
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
    public string PrintUpdateOrDeleteOptionsAndGetSelection()
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
}
