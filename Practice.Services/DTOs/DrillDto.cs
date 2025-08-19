using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class CreateDrillDto
    {
        [Required]
        public int DrillsId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [CustomValidation(typeof(ValidationMethods), nameof(ValidationMethods.ValidateEndDate))]
        public DateTime EndDate { get; set; }
    }

    public class UpdateDrillDto : CreateDrillDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
    }

    public class DrillResponseDto
    {
        public int Id { get; set; }
        public int DrillsId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DrillMinimalDto Drills { get; set; } = null!;
    }

    public class DrillMinimalDto
    {
        public int Id { get; set; }
    }

    public class DrillListDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class DrillSearchDto
    {
        public int Id { get; set; }
        public int DrillsId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class DrillSummaryDto
    {
        public int Id { get; set; }

        // Assume TotalDurationMinutes is computed from StartDate and EndDate
        public int TotalDurationMinutes { get; set; }
    }

    public static class ValidationMethods
    {
        public static ValidationResult? ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as CreateDrillDto;
            if (instance != null && instance.StartDate >= endDate)
            {
                return new ValidationResult("EndDate must be greater than StartDate.");
            }
            return ValidationResult.Success;
        }
    }
}
