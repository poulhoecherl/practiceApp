using Humanizer;
using Practice.Data.Models;
using Practice.Runner;
using Practice.Services.DTOs;
using Practice.Services.Profiles;
using Practice.Services.Services;
using Spectre.Console;
using System;

class Program
{
    private static DataService _dataService = new DataService();

    static async Task Main(string[] args)
    {
        await RunMenuAsync();
    }

    static async Task RunMenuAsync()
    {
        bool exitRequested = false;

        while (!exitRequested)
        {
            Menu.DisplayHeader();

            var choice = Menu.DisplayMenu();

            var subChoice = string.Empty;

            switch (choice)
            {
                case "Sessions":
                    subChoice = Menu.DisplaySessionOptions();
                    if(subChoice.Contains("Create"))
                    {
                        await CreateNewSessionAsync();
                        
                    }
                    else if (subChoice.Contains("Finish"))
                    {
                        var sessionMenu = await GetOpenSessionListMenuAsync();

                        var selectedOption = AnsiConsole.Prompt(sessionMenu);

                        selectedOption.Action?.Invoke();
                    }
                    else if (subChoice.Contains("View"))
                    {
                        DataService ds = new DataService();

                        var sessions = await ds.GetSessions();

                        await DisplaySessionsAsync();
                    }
                    //await CreateNewSessionAsync();
                    break;
                case "Songs":
                    subChoice = Menu.DisplaySongOptions();
                    //await CreateNewSongAsync();
                    break;
                case "Drills":
                    subChoice = Menu.DisplayDrillOptions();
                    //await CreateNewDrillAsync();
                    break;
                case "Exit":
                    exitRequested = true;
                    AnsiConsole.MarkupLine("\n[green]Goodbye! Happy practicing![/]");
                    break;
            }
        }
    }

    private static async Task HandleSessionMenuAction(int sessionId)
    {
        
        // Complete the session based on the ID passed
        var lastSession = await _dataService.FinishSession(sessionId);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
    }

    private static async Task<SelectionPrompt<MenuItem>> GetOpenSessionListMenuAsync()
    {
        // find the session to finish
        
        List<MenuItem> sessionMenuList = new List<MenuItem>();

        var sessionItems = await _dataService.GetSessions();
        foreach(var session in sessionItems)
        {
            // AnsiConsole.WriteLine($"Session ID: {session.Id}, Start Date: {session.StartDate}, End Date: {session.EndDate}");
            sessionMenuList.Add(new MenuItem
            {
                Name = $"[[{session.Id}]] > Start Date: {session.StartDate}, End Date: {session.EndDate}",
                Action = () => { _ = HandleSessionMenuAction(session.Id); }
            });
        }

        var sessionMenu = new SelectionPrompt<MenuItem>()
            .Title("Select a session to finish:")
            .AddChoices(sessionMenuList);

        return sessionMenu;
    }

    static async Task DisplaySessionsAsync()
    {
        AnsiConsole.Clear();

        var ds = new DataService();

        var sessions = await ds.GetSessions();

        // Create a table to display session information
        var sessionTable = new Table()
            .BorderColor(Color.Green)
            .AddColumn("[bold]Session ID[/]")
            .AddColumn("[bold]Start Time[/]")
            .AddColumn("[bold]End Time[/]")
            .AddColumn("[bold]Duration[/]");

        foreach (var session in sessions)
        {
            if (session.EndDate == new DateTime(1901, 1, 1))
            {
                sessionTable.AddRow($"[cyan]{session.Id}[/]"
                , $"[cyan]{session.StartDate:yyyy-MM-dd HH:mm:ss}[/]"
                , $"[red]Incomplete[/]"
                , $"");
            }
            else
            {
                sessionTable.AddRow($"[cyan]{session.Id}[/]"
                    , $"[cyan]{session.StartDate:yyyy-MM-dd HH:mm:ss}[/]"
                    , $"[cyan]{session.EndDate:yyyy-MM-dd HH:mm}[/]"
                    , $"[yellow]{session.Duration}[/]");
            }
        }

        AnsiConsole.Write(sessionTable);
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
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
        var session = new Session();

        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var task = ctx.AddTask("[green]Creating new session...[/]");

                while (!task.IsFinished)
                {
                    var ds = new DataService();

                    await ds.AddSession(session);
                    
                    task.Increment(100);

                    task.StopTask();
                }
            });

        // Create a table to display session information
        var sessionTable = new Table()
            .BorderColor(Color.Green)
            .AddColumn("[bold]Property[/]")
            .AddColumn("[bold]Value[/]")
            .AddRow("Start Time", $"[cyan]{session.StartDate:yyyy-MM-dd HH:mm:ss}[/]");
            
        AnsiConsole.Write(sessionTable);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[green]✓ New session created successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
    }

}
