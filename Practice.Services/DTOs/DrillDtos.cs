using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class DrillDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public string? Category { get; set; }
    }

    public class CreateDrillDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? Description { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
        public int DurationMinutes { get; set; }
        
        [MaxLength(100)]
        public string? Category { get; set; }
    }

    public class UpdateDrillDto : CreateDrillDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
    }
}