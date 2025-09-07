using CodingTracker.Views.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Views.Menus
{
    public class TrackSessionView : ITrackSessionView
    {
        private readonly string _dateFormat;
        public TrackSessionView(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public string RenderMenuAndGetSelection()
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

        public DateTime GetStartTimeFromUser()
        {
            var date = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Please enter a start time using the format [yellow]'yyyy-MM-dd HH:mm:ss'[/]:")
                );

            //Add custom validation for time format

            return date;
        }

        public DateTime GetEndTimeFromUser()
        {
            var date = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Please enter an end time using the format [yellow]'yyyy-MM-dd HH:mm:ss'[/]:")
                );

            //Add custom validation for time format

            return date;
        }

        public void ErrorMessage(string parameter, string message)
        {
            AddNewLines(1);
            AnsiConsole.MarkupInterpolated($"[bold red]ERROR:[/] The value for {parameter} encountered the error: [yellow]{message}[/]");
            AddNewLines(2);
        }

        public void ConfirmationMessage(string valueText)
        {
            AddNewLines(1);
            AnsiConsole.MarkupInterpolated($"[bold green]ACCEPTED[/]: Value set to {valueText}");
            AddNewLines(2);
        }

        public void ActionCompleteMessage(bool isSuccess, string state, string message)
        {
            AddNewLines(1);
            if (isSuccess)
            {
                AnsiConsole.MarkupInterpolated($"[bold green]{state.ToUpper()}![/] {message}");
            }
            else
            {
                AnsiConsole.MarkupInterpolated($"[bold red]{state.ToUpper()}![/] {message}");
            }
            AddNewLines(2);
        }

        private void AddNewLines(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                AnsiConsole.WriteLine();
            }
        }
    }
}
