# Piano Practice Tracker

A .NET console application for tracking daily piano practice sessions.

## Project Overview

This application will help musicians track their daily piano practice sessions, including drill exercises and song practice, with all data stored locally in a JSON format using LiteDB.

## Features

### Core Functionality
- **CRUD Operations**: Create, Read, Update, Delete practice sessions
- **Practice Session Tracking**: Log daily practice sessions with start/end times
- **Drill Management**: Track specific drill exercises with timing
- **Song Practice**: Maintain a list of songs being practiced
- **Local Storage**: Data stored as JSON in LiteDB database file

### Data Model

#### Practice Session
- Practice Date
- Start Time
- End Time
- List of Drills
- List of Songs

#### Drill
- Description
- Start Time
- End Time

#### Song
- Song Name/Title
- Practice Notes (optional)

## Technical Requirements

### Technology Stack
- **.NET Console Application** (Latest stable version)
- **C#** programming language
- **Entity Framework Core** (Current version)
- **LiteDB** for JSON-based local storage
- **Database File**: `practice.db`

### Project Structure
```
D:\source\practiceApp\
├── README.md
├── PracticeApp.csproj
├── Program.cs
├── Models/
│   ├── PracticeSession.cs
│   ├── Drill.cs
│   └── Song.cs
├── Data/
│   ├── PracticeContext.cs
│   └── practice.db (generated)
├── Services/
│   └── PracticeService.cs
└── Utilities/
    └── ConsoleHelper.cs
```

## Development Plan

### Phase 1: Project Setup
1. Create .NET console application
2. Install required NuGet packages:
   - Microsoft.EntityFrameworkCore
   - LiteDB
   - LiteDB.EntityFrameworkCore (if available)
3. Set up project structure

### Phase 2: Data Models
1. Create entity classes (PracticeSession, Drill, Song)
2. Configure Entity Framework context
3. Set up LiteDB integration for JSON storage

### Phase 3: Core Services
1. Implement CRUD operations for practice sessions
2. Create service layer for business logic
3. Add data validation

### Phase 4: User Interface
1. Create console menu system
2. Implement user input handling
3. Add data display formatting
4. Error handling and user feedback

### Phase 5: Testing & Refinement
1. Test all CRUD operations
2. Validate data persistence
3. User experience improvements
4. Performance optimization

## Usage Examples

### Main Menu Options
1. Add New Practice Session
2. View Practice Sessions
3. Edit Practice Session
4. Delete Practice Session
5. View Practice Statistics
6. Exit

### Sample Data Entry Flow
```
Enter practice date (YYYY-MM-DD): 2025-07-03
Enter start time (HH:MM): 14:30
Enter end time (HH:MM): 16:00

Add drills? (y/n): y
Drill 1 - Description: Scales practice
Drill 1 - Start time: 14:30
Drill 1 - End time: 14:45

Add songs? (y/n): y
Song 1: Chopin Nocturne Op. 9 No. 2
Song 2: Bach Invention No. 1
```

## Installation & Setup

1. Clone or create the project in `D:\source\practiceApp`
2. Restore NuGet packages: `dotnet restore`
3. Build the project: `dotnet build`
4. Run the application: `dotnet run`

## Data Storage

- **Database File**: `practice.db` (created automatically)
- **Format**: JSON documents stored in LiteDB
- **Location**: Same directory as application executable
- **Backup**: Manual backup of `practice.db` file recommended

## Future Enhancements

- Export practice data to CSV/Excel
- Practice statistics and analytics
- Goal setting and progress tracking
- Web-based interface
- Cloud synchronization
- Practice session templates

## Contributing

This is a personal practice tracking application. Feel free to fork and modify for your own needs.

## License

Personal use project - modify as needed.
