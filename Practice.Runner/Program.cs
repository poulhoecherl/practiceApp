using Practice.Data.Models;
using Practice.Runner;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;
using Practice.Services.Services;
using Spectre.Console;
using System.Diagnostics;

class Program
{
    private static readonly DataService _dataService = new DataService();

    private static readonly IExcelService _excelService = new ExcelService();

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
                    if (subChoice.Contains("Create"))
                    {
                        await CreateNewSessionAsync();

                    }
                    else if (subChoice.Contains("Finish"))
                    {
                        var sessionMenu = await GetOpenSessionListMenuAsync();

                        Menu.DisplayHeader("Finish Session");

                        var selectedOption = AnsiConsole.Prompt(sessionMenu);

                        selectedOption.Action?.Invoke();
                    }
                    else if (subChoice.Contains("View"))
                    {
                        DataService ds = new DataService();

                        var sessions = await ds.GetSessions();

                        await DisplaySessionsAsync();
                    }
                    else if (subChoice.Contains("Import"))
                    {
                        var importSessions = await ImportSessionsAsync();
                    }
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

    }

    private static async Task HandleReturnToMainMenuAction()
    {
        // nothing
    }

    private static async Task<SelectionPrompt<MenuItem>> GetOpenSessionListMenuAsync()
    {
        // find the session to finish

        List<MenuItem> sessionMenuList = new List<MenuItem>();

        var sessionItems = await _dataService.GetOpenSessions();
        foreach (var session in sessionItems)
        {
            // AnsiConsole.WriteLine($"Session ID: {session.Id}, Start Date: {session.StartDate}, End Date: {session.EndDate}");
            sessionMenuList.Add(new MenuItem
            {
                Name = $"[[{session.Id}]] > Start Date: {session.StartDate}, End Date: {session.EndDate}",
                Action = () => { _ = HandleSessionMenuAction(session.Id); }
            });
        }

        sessionMenuList.Add(new MenuItem
        {
            Name = $"Close",
            Action = () => { _ = HandleReturnToMainMenuAction(); }
        });

        var sessionMenu = new SelectionPrompt<MenuItem>()
            .Title("Select a session to finish:")
            .AddChoices(sessionMenuList);

        return sessionMenu;
    }

    static async Task DisplaySessionsAsync()
    {
        AnsiConsole.Clear();

        Menu.DisplayHeader("Sessions");

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
            .AddRow("Start Time", $"[cyan]{session.PracticeDate:yyyy-MM-dd HH:mm:ss}[/]");

        AnsiConsole.Write(sessionTable);
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine("[green]✓ New session created successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();
    }

    private static async Task<List<SessionDto>> ImportSessionsAsync()
    {
        AnsiConsole.Clear();
        Menu.DisplayHeader("Import Sessions");

        // Simulate importing sessions
        string excelPath = @"C:\temp\PianoPracticeLog.xlsx";
        var sheetNames = await _excelService.GetWorksheetNamesAsync(excelPath);
        var practiceLogList = new List<SessionDto>();

        foreach (var sheet in sheetNames)
        {
            if (!sheet.Contains("Sheet"))
            {
                var logs = await _excelService.ConvertXlsxToSessionDtoColAsync(excelPath, sheet);
                if (logs?.Any() == true)
                {
                    var validLogs = logs.Where(m => m.PracticeDate.Date != DateTime.MinValue.Date);
                    practiceLogList.AddRange(validLogs);
                }
            }
        }

        foreach (var pl in practiceLogList)
        {
            Debug.WriteLine($"{pl}");
            // Now you can save to database using DataService
        }

        Debug.WriteLine($"Imported {practiceLogList.Count} practice logs from Excel.");

        AnsiConsole.MarkupLine("[green]✓ Sessions imported successfully![/]");
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[dim]Press any key to return to main menu...[/]");
        Console.ReadKey();

        return practiceLogList;
    }
}
