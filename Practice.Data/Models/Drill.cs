using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Data.Models
{
    public class Drill : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Required]
        public DateTime RowCreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string RowCreatedBy { get; set; } = string.Empty;

        public DateTime? RowModifiedOn { get; set; }

        public string? RowModifiedBy { get; set; }
    }
}
