using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Views
{
    public static class Messages
    {
        public static void RenderWelcome()
        {
            AnsiConsole.Markup("[bold blue]CODING TRACKER[/]");
            AnsiConsole.Markup("[bold blue]Version 1.0[/]");
            AnsiConsole.Write(new Rule());

        }
        public static void ErrorMessage(string parameter, string message)
        {
            AddNewLines(1);
            AnsiConsole.MarkupInterpolated($"[bold red]ERROR:[/] The value for {parameter} encountered the error: [yellow]{message}[/]");
            AddNewLines(2);
        }

        public static void ConfirmationMessage(string valueText)
        {
            AddNewLines(1);
            AnsiConsole.MarkupInterpolated($"[bold green]ACCEPTED[/]: Value set to {valueText}");
            AddNewLines(2);
        }
        public static void ActionCompleteMessage(bool isSuccess, string state, string message)
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

        private static void AddNewLines(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                AnsiConsole.WriteLine();
            }
        }
    }
}
