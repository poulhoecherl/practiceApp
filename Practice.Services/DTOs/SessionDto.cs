using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class CreateSessionDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [CustomValidation(typeof(SessionValidationMethods), nameof(SessionValidationMethods.ValidateEndDate))]
        public DateTime EndDate { get; set; }
    }

    public class UpdateSessionDto : CreateSessionDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
    }

    public class SessionResponseDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class SessionListDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class SessionSearchDto
    {
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
    }

    public class SessionSummaryDto
    {
        public int Id { get; set; }
        public int DurationMinutes { get; set; }
    }

    public static class SessionValidationMethods
    {
        public static ValidationResult? ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as CreateSessionDto;
            if (instance != null && instance.StartDate >= endDate)
            {
                return new ValidationResult("EndDate must be greater than StartDate.");
            }
            return ValidationResult.Success;
        }
    }
}
