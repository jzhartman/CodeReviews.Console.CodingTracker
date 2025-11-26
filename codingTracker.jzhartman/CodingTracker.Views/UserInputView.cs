using CodingTracker.Models.Entities;
using CodingTracker.Views.Interfaces;
using Microsoft.VisualBasic;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.Views;
public class UserInputView : IUserInputView
{
    private readonly string _dateFormat;
    public UserInputView(string dateFormat)
    {
        _dateFormat = dateFormat;
    }
    public DateTime GetTimeFromUser(string parameterName, bool allowNull = false)
    {
        var timeInput = string.Empty;
        var promptText = GenerateEnterDatePromptText(parameterName, allowNull);
        var errorMessageText = $"[bold red]ERROR:[/] The value you entered does not match the required format!\r\n";


        if (allowNull)
        {
            timeInput = AnsiConsole.Prompt(

                                        new TextPrompt<string>(promptText)
                                        .AllowEmpty()
                                        .ValidationErrorMessage(errorMessageText)
                                        .Validate(input =>
                                        {
                                            if (String.IsNullOrWhiteSpace(input))
                                            {
                                                return ValidationResult.Success();
                                            }
                                            else if (DateTime.TryParseExact(input, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                            {
                                                return ValidationResult.Success();
                                            }
                                            else
                                                return ValidationResult.Error();
                                        }));

            if (timeInput == "") timeInput = DateTime.MinValue.ToString(_dateFormat);
        }

        else timeInput = AnsiConsole.Prompt(
                                        new TextPrompt<string>(promptText)
                                        .ValidationErrorMessage(errorMessageText)
                                        .Validate(input =>
                                        {
                                            if (DateTime.TryParseExact(input, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                            {
                                                return ValidationResult.Success();
                                            }
                                            return ValidationResult.Error();
                                        }));

        return DateTime.ParseExact(timeInput, _dateFormat, CultureInfo.InvariantCulture);
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
        var duration = ConvertTimeFromSecondsToText(session.Duration);

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"Add coding session starting at [yellow]{session.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] and ending at [yellow]{session.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/] with duration [yellow]{duration}[/]?")
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
        var duration = ConvertTimeFromSecondsToText(session.Duration);

        string promptText = $"Confirm deletion of coding session with start time [yellow]{session.StartTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]"
                            + $" and end time [yellow]{session.EndTime.ToString("yyyy-MM-dd HH:mm:ss")}[/]"
                            + $" with duration [yellow]{duration}[/]";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }



    public long GetGoalValueTime(GoalType goalType)
    {
        string promptText = "Please enter the value for the ";
        if (goalType == GoalType.TotalTime) promptText += "total time to code within this timeframe using the format [yellow]d.HH:mm:ss[/]:";
        if (goalType == GoalType.AverageTime) promptText += "average daily time coding within this timeframe using the format [yellow]HH:mm:ss[/]:";

        var goalValue = AnsiConsole.Prompt(
            new TextPrompt<TimeSpan>(promptText));

        return (long)goalValue.TotalSeconds;
    }
    public long GetGoalValueForDaysPerPeriod()
    {
        var goalValue = AnsiConsole.Prompt(
            new TextPrompt<long>($"Please enter the goal value for the days per period:"));

        return goalValue;
    }
    public bool GetAddGoalConfirmationFromUser(GoalModel goal)
    {
        string promptText = $"Add {GenerateGoalConfirmationPromptText( new GoalDTO {Id = 0, Type = goal.Type, StartTime = goal.StartTime,
                                                                                    EndTime = goal.EndTime, Status = goal.Status,
                                                                                    GoalValue = goal.GoalValue, CurrentValue = goal.CurrentValue,
                                                                                    Progress = goal.Progress})}";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"{promptText}?")
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }
    public bool GetDeleteGoalConfirmationFromUser(GoalDTO goal)
    {
        string promptText = $"Confirm deletion of {GenerateGoalConfirmationPromptText(goal)}";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }

    public void PressAnyKeyToContinue()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Markup("[yellow]Press any key to continue...[/]");
        Console.ReadKey(true);
    }



    private string GenerateGoalConfirmationPromptText(GoalDTO goal)
    {
        string valueText = string.Empty;

        if (goal.Type == GoalType.TotalTime || goal.Type == GoalType.AverageTime)
            valueText = TimeSpan.FromSeconds(goal.GoalValue).ToString();
        if (goal.Type == GoalType.DaysPerPeriod)
            valueText = TimeSpan.FromDays(goal.GoalValue).ToString("%d");

        string promptText = "[yellow]" + goal.Type + "[/] goal " +
                            "starting at [yellow]" + goal.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "[/] " +
                            "and ending at [yellow]" + goal.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "[/] " +
                            "with value [yellow]" + valueText + "[/]";
        return promptText;
    }
    private string ConvertTimeFromSecondsToText(double input)
    {
        int miliseconds = TimeSpan.FromSeconds(input).Milliseconds;
        int seconds = TimeSpan.FromSeconds(input).Seconds;

        if ((double)miliseconds / 1000 >= 0.5) seconds++;

        int minutes = TimeSpan.FromSeconds(input).Minutes;
        int hours = TimeSpan.FromSeconds(input).Hours + TimeSpan.FromSeconds(input).Days * 24;

        return $"{hours}:{minutes:00}:{seconds:00}";
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
    private string GenerateEnterDatePromptText(string parameterName, bool allowNull)
    {
        string article = GenerateArticleForDatePromptText(parameterName);
        string promptText = $"Please enter {article} {parameterName} using the format [yellow]'yyyy-MM-dd HH:mm:ss'[/]";

        if (allowNull)
        {
            if (parameterName == "new start time")
                promptText += $".\r\nOr, leave blank and press ENTER to keep the existing start time:";
            else if (parameterName == "new end time")
                promptText += $".\r\nOr, leave blank and press ENTER to keep the existing end time:";
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