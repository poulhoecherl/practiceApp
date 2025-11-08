using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Data.Models
{
    public class Song : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string? Artist { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Genre { get; set; } = "Unknown";

        [Required]
        public DateTime RowCreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string RowCreatedBy { get; set; } = string.Empty;

        public DateTime? RowModifiedOn { get; set; }

        public string? RowModifiedBy { get; set; }

        public bool Deleted { get; set; }
    }
}
