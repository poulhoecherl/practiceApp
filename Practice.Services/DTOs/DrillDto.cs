using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class DrillDto
    {
        [Required]
        public int DrillsId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [CustomValidation(typeof(ValidationMethods), nameof(ValidationMethods.ValidateEndTime))]
        public DateTime EndTime { get; set; }
    }

    public static class ValidationMethods
    {
        public static ValidationResult? ValidateEndTime(DateTime endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as DrillDto;
            if (instance != null && instance.StartTime >= endDate)
            {
                return new ValidationResult("EndTime must be greater than StartTime.");
            }
            return ValidationResult.Success;
        }
    }
}
