using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CodingTracker.Views
{
    public static class ViewHelpers
    {
        public static void RenderWelcome()
        {
            AnsiConsole.Markup("[bold blue]CODING TRACKER[/]");
            AnsiConsole.Markup("[bold blue]Version 1.0[/]");
            AnsiConsole.Write(new Rule());

        }
    }
}
