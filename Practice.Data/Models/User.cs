using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Data.Models
{
    public class User : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Email { get; set; }
        
        // Navigation property - one user can have many sessions
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
        
        // Audit fields from IAuditableEntity
        [Required]
        public DateTime RowCreatedOn { get; set; } = DateTime.UtcNow;
        
        [Required]
        public string RowCreatedBy { get; set; } = string.Empty;
        
        public DateTime? RowModifiedOn { get; set; }
        
        public string? RowModifiedBy { get; set; }
        
        public bool Deleted { get; set; }
        
        // Computed property for full name
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
