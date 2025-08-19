using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class CreateDrillsDto
    {
        public List<CreateDrillDto>? DrillCollection { get; set; }
    }

    public class UpdateDrillsDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }

        public List<CreateDrillDto>? DrillCollection { get; set; }
    }

    public class DrillsResponseDto
    {
        public int Id { get; set; }
        public List<DrillListDto> DrillCollection { get; set; } = new();
    }

    public class DrillsListDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }

    public class DrillsSearchDto
    {
        public int Id { get; set; }
        public int? MinCount { get; set; }
        public int? MaxCount { get; set; }
    }

    public class DrillsSummaryDto
    {
        public int Id { get; set; }
        public int TotalDrills { get; set; }
        public int TotalDurationMinutes { get; set; }
    }
}
