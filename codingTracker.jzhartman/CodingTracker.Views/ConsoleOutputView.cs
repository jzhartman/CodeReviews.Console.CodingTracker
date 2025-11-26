using CodingTracker.Models.Entities;
using CodingTracker.Views.Interfaces;
using Spectre.Console;
using System.Reflection.Metadata;

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
        AnsiConsole.MarkupInterpolated($"[bold red]ERROR:[/] The value for {parameter} encountered the error: [yellow]{message}[/]");
        AddNewLines(2);
    }
    public void ConfirmationMessage(string valueText)
    {
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
    public void GoalCancelledMessage(string action)
    {
        AddNewLines(1);
        AnsiConsole.MarkupInterpolated($"Cancelled {action} of goal!");
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
    public void PrintCodingSessionToUpdateById(CodingSessionDataRecord session, int rowId)
    {
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow(new string[] { $"[blue]{rowId}[/]",
                                        $"{session.StartTime.ToString("yyyy-MM-dd")} [yellow]{session.StartTime.ToString("HH:mm:ss")}[/]",
                                        $"{session.EndTime.ToString("yyyy-MM-dd")} [yellow]{session.EndTime.ToString("HH:mm:ss")}[/]",
                                        $"{ConvertTimeFromSecondsToText(session.Duration)}" });

        AnsiConsole.Write("Updating Record: ");
        AnsiConsole.Write(grid);

        AddNewLines(1);
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
     


    public void NoRecordsMessage(string recordType)
    {
        AnsiConsole.MarkupInterpolated($"No {recordType} records exist!");
        AddNewLines(2);
    }
    public void PrintGoalListAsTable(List<GoalDTO> goals)
    {
        int count = 1;
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(new Text[] {new Text("Id").Centered(),
                                new Text("Start Time").Centered(),
                                new Text("End Time").Centered(),
                                new Text("Status").Centered(),
                                new Text("Type").Centered()});

        foreach (var goal in goals)
        {
            grid.AddRow(new string[] { $"[blue]{count}[/]",
                                        $"{goal.StartTime.ToString("yyyy-MM-dd")} [yellow]{goal.StartTime.ToString("HH:mm:ss")}[/]",
                                        $"{goal.EndTime.ToString("yyyy-MM-dd")} [yellow]{goal.EndTime.ToString("HH:mm:ss")}[/]",
                                        $"{goal.Status}",
                                        $"{goal.Type}"});
            count++;
        }

        AnsiConsole.Write(grid);
        AddNewLines(2);
    }
    public void GoalEvaluationMessage(GoalDTO goal)
    {
        string goalValueText = string.Empty;
        string currentValueText = string.Empty;
        string preamble = string.Empty;


        if (goal.Type == GoalType.TotalTime || goal.Type == GoalType.AverageTime)
        {
            goalValueText = TimeSpan.FromSeconds(goal.GoalValue).ToString();
            currentValueText = TimeSpan.FromSeconds(goal.CurrentValue).ToString();
        }
        if (goal.Type == GoalType.DaysPerPeriod)
        {
            goalValueText = TimeSpan.FromSeconds(goal.GoalValue).TotalDays.ToString();
            currentValueText = TimeSpan.FromSeconds(goal.CurrentValue).TotalDays.ToString();
        }

        if (goal.Status == GoalStatus.Complete)
            preamble = "[bold green]CONGRATULATIONS![/] Successfully completed";
        if (goal.Status == GoalStatus.Failed)
            preamble = "[bold red]FAILED[/] Did not successfully complete";

        string message = $"{preamble} the goal to reach [yellow]{goalValueText}[/] [blue]{goal.Type}[/]\n\r" +
            $"Between the times [green]{goal.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] and [red]{goal.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]\n\r" +
            $"With a total completed value of [yellow]{currentValueText}[/] for a total of [yellow]{goal.Progress:f1}%[/]";

        AnsiConsole.Markup(message);
        AddNewLines(2);
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
