# DTO Implementation with Unit of Work and Repository Pattern

This document explains how to use the Data Transfer Objects (DTOs) with the Unit of Work and Repository pattern in the Practice Services project.

## Overview

This implementation provides a clean separation between your data entities and the data that flows through your API. The DTOs serve different purposes:

- **Create DTOs**: For creating new entities (no ID field)
- **Update DTOs**: For updating existing entities (includes ID)
- **Response DTOs**: For returning detailed data from the API
- **List DTOs**: For returning simplified data in collections
- **Search DTOs**: For search results with specific fields
- **Summary DTOs**: For aggregate data and statistics

## Directory Structure

```
Practice.Services/
├── DTOs/
│   ├── DrillDto.cs          # Drill-related DTOs
│   ├── DrillsDto.cs         # Drills collection DTOs
│   ├── SessionDto.cs        # Session-related DTOs
│   ├── SongDto.cs           # Song-related DTOs
│   ├── SongsDto.cs          # Songs collection DTOs
│   └── MappingService.cs    # Entity-DTO mapping service
├── Services/
│   └── SongService.cs       # Example service implementation
├── Controllers/
│   └── SongsController.cs   # Example API controller
└── Configuration/
    └── ServiceCollectionExtensions.cs # DI setup
```

## Entity-DTO Mappings

### Drill Entity
- **CreateDrillDto**: Creating new drills
- **UpdateDrillDto**: Updating existing drills
- **DrillResponseDto**: Detailed drill information
- **DrillListDto**: Simplified drill information for lists

### Song Entity
- **CreateSongDto**: Creating new songs
- **UpdateSongDto**: Updating existing songs
- **SongResponseDto**: Detailed song information
- **SongListDto**: Simplified song information for lists
- **SongSearchDto**: Search results
- **FavoriteSongDto**: Favorite songs with ratings

## Usage Examples

### 1. Creating a Song

```csharp
// In your controller
[HttpPost]
public async Task<ActionResult<SongResponseDto>> CreateSong([FromBody] CreateSongDto dto)
{
    var createdSong = await _songService.CreateSongAsync(dto);
    return CreatedAtAction(nameof(GetSong), new { id = createdSong.Id }, createdSong);
}

// In your service
public async Task<SongResponseDto> CreateSongAsync(CreateSongDto dto)
{
    // Validate DTO
    if (string.IsNullOrWhiteSpace(dto.Title))
        throw new ArgumentException("Title cannot be empty");

    // Map DTO to entity
    var songEntity = _mappingService.MapToEntity(dto);

    // Use Unit of Work to create
    var createdSong = await _unitOfWork.Songs.CreateAsync(songEntity);
    await _unitOfWork.SaveChangesAsync();

    // Map back to response DTO
    return _mappingService.MapToDto(createdSong);
}
```

### 2. Updating a Song

```csharp
// In your controller
[HttpPut("{id}")]
public async Task<ActionResult<SongResponseDto>> UpdateSong(int id, [FromBody] UpdateSongDto dto)
{
    if (id != dto.Id)
        return BadRequest("ID mismatch");

    var updatedSong = await _songService.UpdateSongAsync(dto);
    if (updatedSong == null)
        return NotFound();

    return Ok(updatedSong);
}

// In your service
public async Task<SongResponseDto?> UpdateSongAsync(UpdateSongDto dto)
{
    var existingSong = await _unitOfWork.Songs.GetByIdAsync(dto.Id);
    if (existingSong == null)
        return null;

    var updatedEntity = _mappingService.MapToEntity(dto, existingSong);
    var updatedSong = await _unitOfWork.Songs.UpdateAsync(updatedEntity);
    await _unitOfWork.SaveChangesAsync();

    return _mappingService.MapToDto(updatedSong);
}
```

### 3. Bulk Operations with Transactions

```csharp
public async Task<IEnumerable<SongResponseDto>> CreateMultipleSongsAsync(IEnumerable<CreateSongDto> dtos)
{
    try
    {
        await _unitOfWork.BeginTransactionAsync();

        var songEntities = dtos.Select(_mappingService.MapToEntity);
        var createdSongs = await _unitOfWork.Songs.CreateRangeAsync(songEntities);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();

        return createdSongs.Select(_mappingService.MapToDto);
    }
    catch (Exception)
    {
        await _unitOfWork.RollbackTransactionAsync();
        throw;
    }
}
```

### 4. Search and Filter Operations

```csharp
public async Task<IEnumerable<SongSearchDto>> SearchSongsByTitleAsync(string title)
{
    var songs = await _unitOfWork.Songs.SearchSongsByTitleAsync(title);
    return songs.Select(song => new SongSearchDto
    {
        Id = song.Id,
        Title = song.Title,
        Artist = song.Artist,
        Album = song.Album,
        Rating = song.Rating,
        LastPlayedDate = song.LastPlayedDate
    });
}
```

## Mapping Service

The `IMappingService` provides centralized mapping between entities and DTOs:

```csharp
public interface IMappingService
{
    // Entity to DTO
    SongResponseDto MapToDto(Song song);
    SongListDto MapToListDto(Song song);
    
    // DTO to Entity
    Song MapToEntity(CreateSongDto dto);
    Song MapToEntity(UpdateSongDto dto, Song existing);
    
    // Collection mappings
    IEnumerable<T> MapToDto<T>(IEnumerable<object> entities) where T : class;
}
```

## Dependency Injection Setup

Register your services in `Program.cs`:

```csharp
// Add to your Program.cs
builder.Services.AddDataAccessServices();  // Unit of Work & Repositories
builder.Services.AddPracticeServices();    // Business Services & DTOs
```

## Validation

DTOs include validation attributes:

```csharp
public class CreateSongDto : SongBaseDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string Artist { get; set; } = string.Empty;
    
    [Range(1, 10)]
    public int? Rating { get; set; }
}
```

## API Endpoints

The example controller provides these endpoints:

- `POST /api/songs` - Create a song
- `GET /api/songs/{id}` - Get song by ID
- `GET /api/songs` - Get all songs
- `PUT /api/songs/{id}` - Update a song
- `DELETE /api/songs/{id}` - Delete a song
- `GET /api/songs/search?title={title}` - Search songs
- `GET /api/songs/favorites` - Get favorite songs
- `GET /api/songs/recent?count={count}` - Get recent songs
- `POST /api/songs/batch` - Create multiple songs
- `DELETE /api/songs/batch` - Delete multiple songs

## Benefits

1. **Separation of Concerns**: DTOs separate API contracts from database entities
2. **Validation**: Input validation at the DTO level
3. **Security**: Only expose necessary fields to the API
4. **Flexibility**: Different DTOs for different use cases
5. **Transaction Management**: Unit of Work manages transactions across repositories
6. **Testability**: Easy to mock services and repositories
7. **Maintainability**: Clear patterns for CRUD operations

## Best Practices

1. **Always validate DTOs** before mapping to entities
2. **Use transactions** for operations that modify multiple entities
3. **Handle exceptions** appropriately in services and controllers
4. **Keep DTOs focused** on specific use cases
5. **Use the mapping service** for consistent entity-DTO conversions
6. **Follow naming conventions** (CreateXDto, UpdateXDto, XResponseDto, etc.)

## Extension Points

To add new entities:

1. Create the corresponding DTO classes
2. Add mapping methods to `IMappingService`
3. Create a service interface and implementation
4. Add a controller for the API endpoints
5. Register services in dependency injection

This pattern scales well and provides a solid foundation for building robust APIs with proper separation of concerns.
