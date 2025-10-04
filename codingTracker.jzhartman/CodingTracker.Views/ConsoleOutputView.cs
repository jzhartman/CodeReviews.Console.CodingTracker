using CodingTracker.Models.Entities;
using CodingTracker.Views.Interfaces;
using Spectre.Console;

namespace CodingTracker.Views;
public class ConsoleOutputView : IConsoleOutputView
{
    public void WelcomeMessage()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupLine("[bold blue]CODING TRACKER[/]");
        AnsiConsole.MarkupLine("[bold blue]Version 1.0[/]");
        AnsiConsole.Write(new Rule());
        AddNewLines(1);
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
    public void ActionCancelledMessage(string action)
    {
        AddNewLines(1);
        AnsiConsole.MarkupInterpolated($"Cancelled {action} of coding session!");
    }
    public void PrintCodingSessionListAsTable(List<CodingSessionDataRecord> sessions)
    {
        int count = 1;
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(new Text[] {new Text("Id").Centered(),
                                new Text("Start Time").Centered(),
                                new Text("End Time").Centered(),
                                new Text("Duration").Centered()});

        foreach (var session in sessions)
        {
            grid.AddRow(new string[] { $"[blue]{count}[/]",
                                        $"{session.StartTime.ToString("yyyy-MM-dd")} [yellow]{session.StartTime.ToString("HH:mm:ss")}[/]",
                                        $"{session.EndTime.ToString("yyyy-MM-dd")} [yellow]{session.EndTime.ToString("HH:mm:ss")}[/]",
                                        $"{ConvertTimeFromSecondsToText(session.Duration)}" });
            count++;
        }

        AnsiConsole.Write(grid);
        AddNewLines(2);
    }
    public void PrintSingleCodingSession(CodingSessionDataRecord session)
    {
        Console.WriteLine($"{session.Id}:\t{session.StartTime} to {session.EndTime} for a duration of {session.Duration}");
    }
    public void PrintReportDataAsTable(ReportModel report)
    {
        AnsiConsole.WriteLine();

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(new string[] { "[bold blue]First Entry:[/]", $"{report.FirstEntry.StartTime.ToString("yyyy-MM-dd")} to {report.FirstEntry.EndTime.ToString("yyyy-MM-dd")}" });
        grid.AddRow(new string[] { "[bold blue]Last Entry:[/]", $"{report.LastEntry.StartTime.ToString("yyyy-MM-dd")} to {report.LastEntry.EndTime.ToString("yyyy-MM-dd")}" });
        grid.AddRow(new string[] { "[bold blue]Total Sessions:[/]", $"{report.SessionCount}" });
        grid.AddRow(new string[] { "[bold blue]Total Time:[/]", $"{ConvertTimeFromSecondsToText(report.TotalTime)}" });
        grid.AddRow(new string[] { "[bold blue]Average Session:[/]", $"{ConvertTimeFromSecondsToText(report.AverageTime)}"});

        AnsiConsole.Write(grid);

        AddNewLines(2);
        AnsiConsole.Write("Press any key to continue... ");
        AnsiConsole.Console.Input.ReadKey(false);
    }




    private void AddNewLines(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            AnsiConsole.WriteLine();
        }
    }
    private string ConvertTimeFromSecondsToText(double input)
    {
        int miliseconds = TimeSpan.FromSeconds(input).Milliseconds;
        int seconds = TimeSpan.FromSeconds(input).Seconds;

        if ((double)miliseconds / 1000 >= 0.5) seconds++;

        int minutes = TimeSpan.FromSeconds(input).Minutes;
        int hours = TimeSpan.FromSeconds(input).Hours + TimeSpan.FromSeconds(input).Days * 24;

        return $"[yellow]{hours,4}[/] hours [yellow]{minutes,2}[/] minutes [yellow]{seconds,2}[/] seconds";
    }
}
