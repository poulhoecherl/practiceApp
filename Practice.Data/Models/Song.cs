using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Practice.Data.Models
{
    public class Song
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Artist { get; set; }
        
        [MaxLength(100)]
        public string? Genre { get; set; } = "Unknown";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
