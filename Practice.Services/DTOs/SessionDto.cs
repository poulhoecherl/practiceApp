using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class SessionDto
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } = 1;

        [Required]
        public DateTime PracticeDate { get; set; }

        [Required]
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [MaxLength(500)]
        [Required]
        public string? Activity { get; set; }

        /// <summary>
        /// Duration in minutes
        /// </summary>
        public int DurationMinutes { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        [Required]
        public DateTime RowCreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string RowCreatedBy { get; set; } = string.Empty;

        public DateTime? RowModifiedOn { get; set; }

        public string? RowModifiedBy { get; set; }  
        
        public string DurationFormatted
        {
            get
            {
                TimeSpan ts = (TimeSpan)(EndTime.GetValueOrDefault() - StartTime.GetValueOrDefault());
                return ts.Humanize(2); // This will now work with the Humanizer library
            }
        }
    }

    public static class SessionValidationMethods
    {
        public static ValidationResult? ValidateEndTime(DateTime endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as SessionDto;
            if (instance != null && instance.StartTime >= endDate)
            {
                return new ValidationResult("EndTime must be greater than StartTime.");
            }
            return ValidationResult.Success;
        }
    }
}
