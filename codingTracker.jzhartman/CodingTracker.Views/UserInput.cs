using CodingTracker.Models.Entities;
using CodingTracker.Views.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker.Views
{
    public class UserInput : IUserInput
    {
        private readonly string _dateFormat;
        public UserInput(string dateFormat)
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

        //public DateTime GetUpdatedStartTimeFromUser(DateTime originalTime)
        //{
        //    AnsiConsole.MarkupInterpolated($"Changing start time from [yellow]{originalTime}[/].");
        //    return GetEndTimeFromUser();
        //}

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
            string promptText = GenerateUpdateSessoinConfirmationPrompt(session, updatedSession);

            var confirmation = AnsiConsole.Prompt(
                new TextPrompt<bool>(promptText)
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice ? "y" : "n"));

            return confirmation;
        }

        private string GenerateUpdateSessoinConfirmationPrompt(CodingSessionDataRecord session, CodingSession updatedSession)
        {
            string prompt = $"Update coding session ";

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
            string article = GetArticle(parameterName);
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

        private string GetArticle(string noun)
        {
            string article = "a";
            char firstLetter = noun.ToLower()[0];
            if (firstLetter == 'a' || firstLetter == 'e' || firstLetter == 'i' || firstLetter == 'o' || firstLetter == 'u') article += "n";

            return article;
        }
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