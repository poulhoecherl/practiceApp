using Practice.Data.Models;
using Practice.Runner;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        await RunMenuAsync();
    }

    static async Task RunMenuAsync()
    {
        bool exitRequested = false;

        while (!exitRequested)
        {
            DisplayHeader();

            var choice = DisplayMenu();

            switch (choice)
            {
                case "Create New Session":
                    await CreateNewSessionAsync();
                    break;
                case "Create New Song":
                    await CreateNewSongAsync();
                    break;
                case "Create New Drill":
                    await CreateNewDrillAsync();
                    break;
                case "List All Songs":
                    await ListAllSongsAsync();
                    break;

                case "Generate Song":
                    await GenSongJsonAsync();
                    break;

                case "Exit":
                    exitRequested = true;
                    AnsiConsole.MarkupLine("\n[green]Goodbye! Happy practicing![/]");
                    break;
            }
        }
    }

    private static async Task GenSongJsonAsync()
    {
        var song = new Song
        {
            Id = 1,
            SongsId = 1,
            Name = "All of Me",
            Artist = "John Legend",
            Genre = "Pop",
            Duration = TimeSpan.FromMinutes(4.5)
        };


    }

    private static async Task ListAllSongsAsync()
    {
        AnsiConsole.Clear();

        var drillPanel = new Panel("[bold cyan]LIST ALL SONGS[/]")
            .Header("[yellow]🎯 Song Management[/]")
            .BorderColor(Color.Cyan1);

        AnsiConsole.Write(drillPanel);
        AnsiConsole.WriteLine();

        // Simulate loading songs with a progress bar
        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task = ctx.AddTask("[green]Loading songs...[/]");
                while (!task.IsFinished)
                {
                    await Task.Delay(100);
                    task.Increment(10);
                }
            });

        // Simulated song data
        var songs = new List<Song>
        {
            new Song { Id = 1, SongsId = 1, Name = "All of Me"},
            new Song { Id = 2, SongsId = 1 },
            new Song { Id = 3, SongsId = 2 }
        };

    }

    static void DisplayHeader()
    {
        AnsiConsole.Clear();

        // Create a fancy panel for the app title
        var titlePanel = new Panel("[bold cyan]PRACTICE APP[/]")
            .Header("[yellow]♪ Music Practice Management ♪[/]") // Corrected method name
            .BorderColor(Color.Cyan1)
            .Padding(1, 0);

        AnsiConsole.Write(titlePanel);
        AnsiConsole.WriteLine();

        // Display current time with styling
        var timeTable = new Table()
            .BorderColor(Color.Yellow)
            .AddColumn(new TableColumn("[yellow]Current Time[/]").Centered())
            .AddRow($"[bold]{DateTime.Now:dddd, MMMM dd, yyyy - hh:mm:ss tt}[/]");

        AnsiConsole.Write(timeTable);
        AnsiConsole.WriteLine();
    }

    static SelectionPrompt<MenuItem> SessionMenu = new SelectionPrompt<MenuItem>()
        .Title("Category A Options:")
        .AddChoices(new List<MenuItem>
        {
            new() { Name = "Create New Session", Action = () => AnsiConsole.WriteLine("Create a New Session") },
            new() { Name = "Display Sessions", Action = () => AnsiConsole.WriteLine("Display Sessions") },
            new() { Name = "Export Sessions", Action = () => AnsiConsole.WriteLine("Export Sessions") },
            new() { Name = "Import Sessions", Action = () => AnsiConsole.WriteLine("Import Sessions") },
            new() { Name = "Back to Main Menu", Action = () => DisplayMenu() } // Go back to the main menu
        });

    static void ShowSessionMenu()
    {

    }

    static void ShowSongMenu()
    {

    }

    static void ShowDrillMenu()
    {

    }

    static string DisplayMenu()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]═══ MAIN MENU ═══[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Session",
                    "Song",
                    "Drill",
                    "Export",
                    "Import",
                    "Exit"
                })
                .UseConverter(choice => choice switch
                {
                    "Create New Session" => "🎵 Create New Session",
                    "Create New Song" => "🎶 Create New Song",
                    "Create New Drill" => "🎯 Create New Drill",
                    "List All Songs" => "🎶 List All Songs",
                    "Generate Song" => "🎶 Generate Song",
                    "Exit" => "🚪 Exit",
                    _ => choice
                }));

        return choice;
    }

    static async Task CreateNewSessionAsync()
    {
        AnsiConsole.Clear();

        var sessionPanel = new Panel("[bold cyan]CREATE NEW SESSION[/]")
            .Header("[yellow]🎵 Session Management[/]") // Corrected method name
            .BorderColor(Color.Cyan1);

        AnsiConsole.Write(sessionPanel);
        AnsiConsole.WriteLine();

        // Show progress for creating session
        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task = ctx.AddTask("[green]Creating new session...[/]");

                while (!task.IsFinished)
                {
                    await Task.Delay(100);
                    task.Increment(20);
                }
            });

        var session = new Session
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(1) // Default 1 hour session
        };

        // Create a table to display session information
        var sessionTable = new Table()
            .BorderColor(Color.Green)
            .AddColumn("[bold]Property[/]")
            .AddColumn("[bold]Value[/]")
            .AddRow("Start Time", $"[cyan]{session.StartDate:yyyy-MM-dd HH:mm:ss}[/]")
            .AddRow("End Time", $"[cyan]{session.EndDate:yyyy-MM-dd HH:mm:ss}[/]")
            .AddRow("Duration", $"[yellow]{(session.EndDate - session.StartDate).TotalMinutes} minutes[/]");

        AnsiConsole.Write(sessionTable);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[green]✓ New session created successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
    }

    static async Task CreateNewSongAsync()
    {
        AnsiConsole.Clear();

        var songPanel = new Panel("[bold cyan]CREATE NEW SONG[/]")
            .Header("[yellow]🎶 Song Management[/]") // Corrected method name
            .BorderColor(Color.Cyan1);

        AnsiConsole.Write(songPanel);
        AnsiConsole.WriteLine();

        // Get song title with validation
        var title = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Enter song title:[/]")
                .DefaultValue("Untitled Song")
                .ValidationErrorMessage("[red]Please enter a valid song title[/]")
                .Validate(title => !string.IsNullOrWhiteSpace(title)));

        // Get artist name with validation
        var artist = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Enter artist name:[/]")
                .DefaultValue("Unknown Artist")
                .ValidationErrorMessage("[red]Please enter a valid artist name[/]")
                .Validate(artist => !string.IsNullOrWhiteSpace(artist)));

        // Show progress for creating song
        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task = ctx.AddTask("[green]Creating new song...[/]");

                while (!task.IsFinished)
                {
                    await Task.Delay(80);
                    task.Increment(25);
                }
            });

        var song = new Song
        {
            Id = new Random().Next(1000, 9999), // Temporary ID for demo
            SongsId = 1 // Default collection ID
        };

        // Create a fancy table to display song information
        var songTable = new Table()
            .BorderColor(Color.Magenta1)
            .AddColumn(new TableColumn("[bold]Property[/]").LeftAligned())
            .AddColumn(new TableColumn("[bold]Value[/]").LeftAligned())
            .AddRow("Song Title", $"[cyan]{title}[/]")
            .AddRow("Artist", $"[cyan]{artist}[/]")
            .AddRow("Song ID", $"[yellow]{song.Id}[/]")
            .AddRow("Collection ID", $"[yellow]{song.SongsId}[/]");

        AnsiConsole.Write(songTable);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[green]✓ New song created successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
    }

    static async Task CreateNewDrillAsync()
    {
        AnsiConsole.Clear();

        var drillPanel = new Panel("[bold cyan]CREATE NEW DRILL[/]")
            .Header("[yellow]🎯 Drill Management[/]") // Corrected method name
            .BorderColor(Color.Cyan1);

        AnsiConsole.Write(drillPanel);
        AnsiConsole.WriteLine();

        // Get drill name with validation
        var drillName = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Enter drill name:[/]")
                .DefaultValue("Practice Drill")
                .ValidationErrorMessage("[red]Please enter a valid drill name[/]")
                .Validate(name => !string.IsNullOrWhiteSpace(name)));

        // Get duration with validation
        var duration = AnsiConsole.Prompt(
            new TextPrompt<int>("[green]Enter duration in minutes:[/]")
                .DefaultValue(15)
                .ValidationErrorMessage("[red]Please enter a valid duration (1-300 minutes)[/]")
                .Validate(duration => duration >= 1 && duration <= 300));

        // Show progress for creating drill
        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task = ctx.AddTask("[green]Creating new drill...[/]");

                while (!task.IsFinished)
                {
                    await Task.Delay(75);
                    task.Increment(20);
                }
            });

        var drill = new Drill
        {
            Id = new Random().Next(1000, 9999), // Temporary ID for demo
            DrillsId = 1, // Default collection ID
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMinutes(duration)
        };

        // Create a table to display drill information
        var drillTable = new Table()
            .BorderColor(Color.Orange3)
            .AddColumn(new TableColumn("[bold]Property[/]").LeftAligned())
            .AddColumn(new TableColumn("[bold]Value[/]").LeftAligned())
            .AddRow("Drill Name", $"[cyan]{drillName}[/]")
            .AddRow("Drill ID", $"[yellow]{drill.Id}[/]")
            .AddRow("Collection ID", $"[yellow]{drill.DrillsId}[/]")
            .AddRow("Start Time", $"[cyan]{drill.StartDate:yyyy-MM-dd HH:mm:ss}[/]")
            .AddRow("End Time", $"[cyan]{drill.EndDate:yyyy-MM-dd HH:mm:ss}[/]")
            .AddRow("Duration", $"[magenta]{duration} minutes[/]");

        AnsiConsole.Write(drillTable);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[green]✓ New drill created successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
    }
}
