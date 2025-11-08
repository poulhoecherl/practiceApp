using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data.Models
{
    public class Session_Drill : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int SessionId { get; set; }
        
        [Required]
        public int DrillId { get; set; }

        // Navigation properties
        public Session Session { get; set; } = null!;
        public Drill Drill { get; set; } = null!;

        // Additional tracking properties
        public DateTime? CompletedAt { get; set; }
        public int? PerformanceRating { get; set; } // 1-10 scale
        public string? Notes { get; set; }

        // Audit properties from IAuditableEntity
        [Required]
        public DateTime RowCreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string RowCreatedBy { get; set; } = string.Empty;

        public DateTime? RowModifiedOn { get; set; }

        public string? RowModifiedBy { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
