using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.Data.Models
{
    public class Drill
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? Description { get; set; }
        
        /// <summary>
        /// Duration in minutes
        /// </summary>
        public int DurationMinutes { get; set; }
        
        [MaxLength(100)]
        public string? Category { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
