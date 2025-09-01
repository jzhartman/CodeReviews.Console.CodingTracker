using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Views.Menus
{
    public class TrackSessionView
    {
        public static string RenderTrackSessionMenuAndGetSelection()
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
    }
}
