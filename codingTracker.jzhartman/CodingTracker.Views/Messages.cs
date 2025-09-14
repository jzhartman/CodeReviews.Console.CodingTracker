using Spectre.Console;

namespace CodingTracker.Views;
public static class Messages
{
    public static void RenderWelcome()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupLine("[bold blue]CODING TRACKER[/]");
        AnsiConsole.MarkupLine("[bold blue]Version 1.0[/]");
        AnsiConsole.Write(new Rule());
    }

    public static void RenderLocation(string location)
    {
        AnsiConsole.MarkupLineInterpolated($"[bold]{location.ToUpper()}[/]");
    }
    public static void Error(string parameter, string message)
    {
        AddNewLines(1);
        AnsiConsole.MarkupInterpolated($"[bold red]ERROR:[/] The value for {parameter} encountered the error: [yellow]{message}[/]");
        AddNewLines(2);
    }

    public static void Confirmation(string valueText)
    {
        AddNewLines(1);
        AnsiConsole.MarkupInterpolated($"[bold green]ACCEPTED[/]: Value set to {valueText}");
        AddNewLines(2);
    }
    public static void ActionComplete(bool isSuccess, string state, string message)
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

    public static void ActionCancelled(string action)
    {
        AddNewLines(1);
        AnsiConsole.MarkupInterpolated($"Cancelled {action} of coding session!");
    }

    private static void AddNewLines(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            AnsiConsole.WriteLine();
        }
    }
}
