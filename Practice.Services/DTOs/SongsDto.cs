using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class CreateSongDto
    {
        [Required]
        public int SongsId { get; set; }
    }

    public class UpdateSongDto : CreateSongDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
    }

    public class SongResponseDto
    {
        public int Id { get; set; }
        public int SongsId { get; set; }

        public string Name { get; set; }

        public SongMinimalDto Songs { get; set; } = null!;
    }

    public class SongMinimalDto
    {
        public int Id { get; set; }
    }

    public class SongListDto
    {
        public int Id { get; set; }
        public int SongsId { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public class SongSearchDto
    {
        public int Id { get; set; }
        public int SongsId { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public class SongSummaryDto
    {
        public int Id { get; set; }

    }

    public class CreateSongsDto
    {
        public List<CreateSongDto>? SongCollection { get; set; }
    }

    public class UpdateSongsDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        public List<CreateSongDto>? SongCollection { get; set; }
    }

    public class SongsResponseDto
    {
        public int Id { get; set; }
        public List<SongListDto> SongCollection { get; set; } = new();
    }

    public class SongsListDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }

    public class SongsSearchDto
    {
        public int Id { get; set; }
        public int? MinCount { get; set; }
        public int? MaxCount { get; set; }
    }

    public class SongsSummaryDto
    {
        public int Id { get; set; }
        public int TotalSongs { get; set; }
        public int TotalDurationMinutes { get; set; }
    }
}
