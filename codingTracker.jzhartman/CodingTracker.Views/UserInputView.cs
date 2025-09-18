using CodingTracker.Models.Entities;
using CodingTracker.Views.Interfaces;
using Spectre.Console;

namespace CodingTracker.Views;
public class UserInputView : IUserInputView
{
    private readonly string _dateFormat;
    public UserInputView(string dateFormat)
    {
        _dateFormat = dateFormat;
    }
    public DateTime GetTimeFromUser(string parameterName, string nullBehavior = "", bool allowNull = false)
    {
        var date = new DateTime();
        var promptText = GenerateEnterDatePromptText(parameterName, nullBehavior, allowNull);

        if (allowNull) date = AnsiConsole.Prompt(
                                        new TextPrompt<DateTime>(promptText)
                                        .AllowEmpty());

        else date = AnsiConsole.Prompt(
                                        new TextPrompt<DateTime>(promptText));
        

        //Add custom validation for time format

        return date;
    }

    public string StartStopwatch()
    {
        AnsiConsole.WriteLine();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Press the ENTER key when you are ready to begin your coding session.")
                .AddChoices(new[]
                {
                    "Start Stopwatch",
                })
            );

        return selection;
    }

    public string StopStopwatch()
    {
        AnsiConsole.WriteLine();
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Press the ENTER key when you are ready to end your coding session.")
                .AddChoices(new[]
                {
                    "Stop Stopwatch",
                })
            );

        return selection;
    }

    public int GetRecordIdFromUser(string action, int max)
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<int>($"Please enter the [yellow]ID[/] of the record you wish to {action.ToLower()}:")
            .Validate(input => 
            {
                if (input < 1) return Spectre.Console.ValidationResult.Error("Too low");
                else if (input > max) return Spectre.Console.ValidationResult.Error("Too high");
                else return Spectre.Console.ValidationResult.Success();
            }));

        return id;
    }
    public bool GetAddSessionConfirmationFromUser(CodingSession session)
    {
        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Add coding session starting at [yellow]{session.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] and ending at [yellow]{session.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] with duration [yellow]{session.DurationText}[/]?")
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }

    public bool GetUpdateSessionConfirmationFromUser(CodingSessionDataRecord session, CodingSession updatedSession)
    {
        string promptText = GenerateUpdateSessionConfirmationPrompt(session, updatedSession);

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }
    public bool GetDeleteSessionConfirmationFromUser(CodingSessionDataRecord session)
    {
        string promptText = $"Confirm deletion of coding session with start time [yellow]{session.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]"
                            + $" and end time [yellow]{session.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]"
                            + $" with duration [yellow]{TimeSpan.FromSeconds(session.Duration).ToString()}[/]";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }

    private string GenerateUpdateSessionConfirmationPrompt(CodingSessionDataRecord session, CodingSession updatedSession)
    {
        string prompt = $"Update coding session ";

        if (session.StartTime == updatedSession.StartTime && session.EndTime == updatedSession.EndTime)
            return $"No changes were made to the coding session with start time [yellow]{session.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] and end time [yellow]{session.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]";

        if (session.StartTime != updatedSession.StartTime)
            prompt += $"start time from [yellow]{session.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] to [green]{updatedSession.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]";

        if (session.StartTime != updatedSession.StartTime && session.EndTime != updatedSession.EndTime)
            prompt += " and ";

        if (session.EndTime != updatedSession.EndTime)
            prompt += $"end time from [yellow]{session.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] to [green]{updatedSession.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]";

        prompt += $" for a new duration of [green]{updatedSession.DurationText}[/].";

        return prompt;
    }
    private string GenerateEnterDatePromptText(string parameterName, string nullBehavior, bool allowNull)
    {
        string article = GenerateArticleForDatePromptText(parameterName);
        string promptText = $"Please enter {article} {parameterName} using the format [yellow]'yyyy-MM-dd HH:mm:ss'[/]";

        if (allowNull)
        {
            promptText += $".\r\nPress enter with blank line to use {nullBehavior}:";
        }
        else
        {
            promptText += ":";
        }

        return promptText;
    }
    private string GenerateArticleForDatePromptText(string noun)
    {
        string article = "a";
        char firstLetter = noun.ToLower()[0];
        if (firstLetter == 'a' || firstLetter == 'e' || firstLetter == 'i' || firstLetter == 'o' || firstLetter == 'u') article += "n";

        return article;
    }
}


/*
    // Define the target date format.
    const string dateFormat = "yyyy-MM-dd";
    
    // Prompt for a date with validation.
    var dateString = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter a date in [green]yyyy-MM-dd[/] format:")
            .PromptStyle("yellow")
            .ValidationErrorMessage($"[red]Invalid date format. Please use {dateFormat}.[/]")
            .Validate(input =>
            {
                // Use DateTime.TryParseExact for robust format checking.
                if (DateTime.TryParseExact(input, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    return ValidationResult.Success();
                }
                
                return ValidationResult.Error();
            }));

    // After successful validation, parse the string into a DateTime object.
    DateTime parsedDate = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);

    AnsiConsole.MarkupLine($"You entered: [green]{parsedDate:yyyy-MM-dd}[/]");
*/