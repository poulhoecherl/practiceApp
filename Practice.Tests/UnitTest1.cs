using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Services;
using Practice.Services.Interfaces;
using System.Text;
using System.Diagnostics;
using Practice.Data.Views;

namespace Practice.Tests;

public class MappingServiceTests
{
    private readonly IMappingService _mappingService;
    private readonly IExcelService _excelService;

    public MappingServiceTests()
    {
        _mappingService = new MappingService();
        _excelService = new ExcelService();
    }

    [Fact]
    public async Task ImportPracticeLogFromExcel()
    {
        string excelPath = @"C:\temp\PianoPracticeLog.xlsx";

        StringBuilder csv = new();

        var sheetNames = await _excelService.GetWorksheetNamesAsync(excelPath);

        List<SessionDto> practiceLogList = new();

        DateTime emptyDate = new DateTime(1, 1, 1);

        foreach (var sheet in sheetNames)
        {
            if (!sheet.Contains("Sheet"))
            {
                var logs = await _excelService.ConvertXlsxToSessionDtoColAsync(excelPath, sheet);

                if(logs != null)
                {
                    var logsx = logs.Where(m => m.PracticeDate.Date != emptyDate.Date);
                    practiceLogList.AddRange(logsx);
                }
            }
        }
        
        Assert.NotNull(practiceLogList);

        foreach(var pl in practiceLogList)
        {
            //Debug.WriteLine($"{pl.ToString()}");
            // Insert into Sessions table

        }

        Debug.WriteLine($"Imported {practiceLogList.Count} practice logs from Excel.");
    }

    [Fact]
    public void MapToDto_Song_ShouldMapCorrectly()
    {
        // Arrange
        var song = new Song
        {
            Id = 1,
            SongsId = 2,
            Name = "Test Song",
            Artist = "Test Artist",
            Genre = "Rock",
        };

        // Act
        var result = _mappingService.MapToDto(song);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(2, result.SongsId);
        Assert.Equal("Test Song", result.Name);
    }

    [Fact]
    public void MapToDto_Song_WithNullEntity_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _mappingService.MapToDto((Song)null));
    }

    [Fact]
    public void MapToEntity_CreateSongDto_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new CreateSongDto
        {
            SongsId = 2
        };

        // Act
        var result = _mappingService.MapToEntity(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.SongsId);
    }

    [Fact]
    public void MapToEntity_CreateSongDto_WithNullDto_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _mappingService.MapToEntity((CreateSongDto)null));
    }

    [Fact]
    public void MapToEntity_UpdateSongDto_ShouldUpdateExistingEntity()
    {
        // Arrange
        var existingSong = new Song
        {
            Id = 1,
            SongsId = 1,
            Name = "Old Song"
        };

        var updateDto = new UpdateSongDto
        {
            Id = 1,
            SongsId = 2
        };

        // Act
        _mappingService.MapToEntity(updateDto, existingSong);

        // Assert
        Assert.Equal(1, existingSong.Id); // Id should remain the same
        Assert.Equal(2, existingSong.SongsId); // SongsId should be updated
    }

    [Fact]
    public void MapToSongListDto_ShouldMapCorrectly()
    {
        // Arrange
        var song = new Song
        {
            Id = 1,
            SongsId = 2,
            Name = "Test Song"
        };

        // Act
        var result = _mappingService.MapToSongListDto(song);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(2, result.SongsId);
        Assert.Equal("Test Song", result.Name);
    }

    [Fact]
    public void MapToDto_Songs_ShouldMapCorrectly()
    {
        // Arrange
        var songs = new Songs
        {
            Id = 1,
            SongCollection = new List<Song>
            {
                new Song { Id = 1, SongsId = 1, Name = "Song 1" },
                new Song { Id = 2, SongsId = 1, Name = "Song 2" }
            }
        };

        // Act
        var result = _mappingService.MapToDto(songs);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(2, result.SongCollection.Count);
        Assert.Equal("Song 1", result.SongCollection[0].Name);
        Assert.Equal("Song 2", result.SongCollection[1].Name);
    }

    [Fact]
    public void MapToEntity_CreateSongsDto_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new CreateSongsDto
        {
            SongCollection = new List<CreateSongDto>
            {
                new CreateSongDto { SongsId = 1 },
                new CreateSongDto { SongsId = 1 }
            }
        };

        // Act
        var result = _mappingService.MapToEntity(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.SongCollection.Count);
        Assert.All(result.SongCollection, song => Assert.Equal(1, song.SongsId));
    }

    [Fact]
    public void MapToEntity_CreateSongsDto_WithNullSongCollection_ShouldCreateEmptyList()
    {
        // Arrange
        var dto = new CreateSongsDto
        {
            SongCollection = null
        };

        // Act
        var result = _mappingService.MapToEntity(dto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.SongCollection);
        Assert.Empty(result.SongCollection);
    }

    [Fact]
    public void MapToSongsSummaryDto_ShouldCalculateTotalSongsCorrectly()
    {
        // Arrange
        var songs = new Songs
        {
            Id = 1,
            SongCollection = new List<Song>
            {
                new Song { Id = 1, SongsId = 1 },
                new Song { Id = 2, SongsId = 1 },
                new Song { Id = 3, SongsId = 1 }
            }
        };

        // Act
        var result = _mappingService.MapToSongsSummaryDto(songs);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(3, result.TotalSongs);
        Assert.Equal(0, result.TotalDurationMinutes); // As per current implementation
    }
}

public class DataAnnotationTests
{
    [Fact]
    public void CreateSongDto_ShouldRequireSongsId()
    {
        // Arrange
        var dto = new CreateSongDto();
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("SongsId"));
    }

    [Fact]
    public void CreateSongDto_WithValidData_ShouldPassValidation()
    {
        // Arrange
        var dto = new CreateSongDto
        {
            SongsId = 1
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void UpdateSongDto_ShouldRequireValidId()
    {
        // Arrange
        var dto = new UpdateSongDto
        {
            Id = 0, // Invalid ID
            SongsId = 1
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Id"));
    }

    [Fact]
    public void UpdateSongDto_WithValidData_ShouldPassValidation()
    {
        // Arrange
        var dto = new UpdateSongDto
        {
            Id = 1,
            SongsId = 1
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void UpdateSongsDto_ShouldRequireValidId()
    {
        // Arrange
        var dto = new UpdateSongsDto
        {
            Id = -1, // Invalid ID
            SongCollection = new List<CreateSongDto>()
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Id"));
    }

    [Fact]
    public void UpdateSongsDto_WithValidData_ShouldPassValidation()
    {
        // Arrange
        var dto = new UpdateSongsDto
        {
            Id = 1,
            SongCollection = new List<CreateSongDto> 
            {
                new CreateSongDto { SongsId = 1 }
            }
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void UpdateSongDto_WithInvalidId_ShouldFailValidation(int invalidId)
    {
        // Arrange
        var dto = new UpdateSongDto
        {
            Id = invalidId,
            SongsId = 1
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains("Id") && r.ErrorMessage == "Id must be greater than 0");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(int.MaxValue)]
    public void UpdateSongDto_WithValidId_ShouldPassValidation(int validId)
    {
        // Arrange
        var dto = new UpdateSongDto
        {
            Id = validId,
            SongsId = 1
        };
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(dto, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}
