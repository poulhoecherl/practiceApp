using Practice.Data.Models;
using Spectre.Console;

class Menu
{
    public static void DisplayHeader()
    {
        AnsiConsole.Clear();

        // Display current time with styling
        var timeTable = new Table()
            .BorderColor(Color.Yellow)
            .AddColumn(new TableColumn("[yellow]Main Menu[/]").Centered());

        AnsiConsole.Write(timeTable);
        AnsiConsole.WriteLine();
    }

    public static void DisplayHeader(string title)
    {
        AnsiConsole.Clear();

        // Display current time with styling
        var displayTable = new Table()
            .BorderColor(Color.Yellow)
            .AddColumn(new TableColumn($"[yellow]{title}[/]").Centered());

        AnsiConsole.Write(displayTable);
        AnsiConsole.WriteLine();
    }

    public static string DisplayMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
                    "Sessions",
                    "Songs",
                    "Drills",
                    "Exit"
                })
                .UseConverter(choice => choice switch
                {
                    "Create New Session" => "🎵 Create New Session",
                    "Create New Song" => "🎶 Create New Song",
                    "Create New Drill" => "🎯 Create New Drill",
                    "Exit" => "Exit",
                    _ => choice
                }));

        return choice;
    }

    public static string DisplaySessionOptions()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
                "Create New Session",
                "Finish a Session",
                "View All Sessions"
                })
                .UseConverter(choice => choice switch
                {
                    "Create New Session" => "🎵 Create New Session",
                    "Finish a Session" => "🎵 Finish a Session",
                    "View All Sessions" => "🎶 View All Sessions",
                    _ => choice
                }));

        return choice;
    }

    public static string DisplaySongOptions()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
            "Create New Song",
            "View All Songs"
                })
                .UseConverter(choice => choice switch
                {
                    "Create New Song" => "🎵 Create New Song",
                    "View All Songs" => "🎶 View All Songs",
                    _ => choice
                }));

        return choice;
    }

    public static string DisplayDrillOptions()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
        "Create New Drill",
        "View All Drills"
                })
                .UseConverter(choice => choice switch
                {
                    "Create New Drill" => "🎵 Create New Drill",
                    "View All Drills" => "🎶 View All Drills",
                    _ => choice
                }));

        return choice;
    }
}
