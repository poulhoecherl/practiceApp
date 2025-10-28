using System.ComponentModel.DataAnnotations;

namespace Practice.Services.DTOs
{
    public class SessionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 1;
        public DateTime PracticeDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Activity { get; set; }
        public int DurationMinutes { get; set; }
        public string? Notes { get; set; }
        
        public override string ToString()
        {
            return $"{PracticeDate:yyyy-MM-dd} - {Activity} - {DurationMinutes} mins - Notes: {Notes}";
        }
    }

}