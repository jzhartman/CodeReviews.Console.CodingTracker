using CodingTracker.Views.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Views
{
    public class UserInput : IUserInput
    {
        private readonly string _dateFormat;
        public UserInput(string dateFormat)
        {
            _dateFormat = dateFormat;
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
    }
}
