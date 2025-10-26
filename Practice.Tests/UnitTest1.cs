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

namespace Practice.Tests;

public class MappingServiceTests
{
    private readonly IMappingService _mappingService;
    private readonly IExcelService _excelService;

    public MappingServiceTests()
    {
        //_mappingService = new MappingService();
        _excelService = new ExcelService();
    }

    [Fact]
    public async Task ImportPracticeLogFromExcel()
    {
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
        
        Assert.NotNull(practiceLogList);
        
        foreach (var pl in practiceLogList)
        {
            Debug.WriteLine($"{pl}");
            // Now you can save to database using DataService
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
            //SongsId = 2,
            Name = "Test Song",
            Artist = "Test Artist",
            Genre = "Rock",
        };

        // Act
        SongDto result = new(); // _mappingService.MapToDto(song);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Song", result.Name);
    }



    [Fact]
    public void MapToEntity_UpdateSongDto_ShouldUpdateExistingEntity()
    {
        // Arrange
        var existingSong = new Song
        {
            Id = 1,
            //SongsId = 1,
            Name = "Old Song"
        };

        var updateDto = new UpdateSongDto
        {
            Id = 1,
        };

        // Act
        //_mappingService.MapToEntity(updateDto, existingSong);

        // Assert
        //Assert.Equal(1, existingSong.Id); // Id should remain the same
    }

    [Fact]
    public void MapToSongListDto_ShouldMapCorrectly()
    {
        // Arrange
        var song = new Song
        {
            Id = 1,
            //SongsId = 2,
            Name = "Test Song"
        };

        // Act
        SongDto result = new(); // _mappingService.MapToSongListDto(song);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        //Assert.Equal(2, result.SongsId);
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
                //new Song { Id = 1, SongsId = 1, Name = "Song 1" },
                //new Song { Id = 2, SongsId = 1, Name = "Song 2" }
            }
        };

        // Act
        SongDto result = new(); // _mappingService.MapToDto(songs);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        //Assert.Equal(2, result.SongCollection.Count);
        //Assert.Equal("Song 1", result.SongCollection[0].Name);
        //Assert.Equal("Song 2", result.SongCollection[1].Name);
    }
}
