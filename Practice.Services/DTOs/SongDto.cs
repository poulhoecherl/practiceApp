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

}