using CodingTracker.Models.Entities;
using Spectre.Console;

namespace CodingTracker.Views;
public static class CodingSessionView
{
    public static void RenderCodingSessions(List<CodingSessionDataRecord> sessions)
    {
        int count = 1;
        Console.WriteLine("A list of coding sessions: ");

        foreach (var session in sessions)
        {
            Console.WriteLine($"{count}:\t{session.StartTime} to {session.EndTime} for a duration of {session.Duration}");
            count++;
        }
    }

    public static void RenderCodingSession(CodingSessionDataRecord session)
    {
        Console.WriteLine($"{session.Id}:\t{session.StartTime} to {session.EndTime} for a duration of {session.Duration}");
    }

    public static void RenderReportData(ReportModel report)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLineInterpolated($"[bold blue]First Entry:[/]\t{report.FirstEntry.StartTime} to {report.FirstEntry.EndTime}");
        AnsiConsole.MarkupLineInterpolated($"[bold blue]Last Entry:[/]\t{report.LastEntry.StartTime} to {report.LastEntry.EndTime}");
        AnsiConsole.MarkupLineInterpolated($"[bold blue]Total Sessions:[/]\t{report.SessionCount}");
        AnsiConsole.MarkupLineInterpolated($"[bold blue]Total Time:[/]\t{report.TotalTimeText}");
        AnsiConsole.MarkupLineInterpolated($"[bold blue]Average Session:[/]\t{report.AverageTimeText}");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Press any key to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }


}
