using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Artist { get; set; }
        public string? Genre { get; set; }
    }

    public class CreateSongDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Artist { get; set; }
        
        [MaxLength(100)]
        public string? Genre { get; set; }
    }

    public class UpdateSongDto : CreateSongDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
    }
}