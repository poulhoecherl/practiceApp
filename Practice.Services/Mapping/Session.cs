using Practice.Data.Models;
using Practice.Services.DTOs;


namespace Practice.Services.Mapping
{
    public class SessionMapper
    {
        public Session MapToEntity(SessionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Session
            {
                Id = dto.Id,
                UserId = dto.UserId,  // This is what should happen to your dtoId
                PracticeDate = dto.PracticeDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Activity = dto.Activity,
                DurationMinutes = dto.DurationMinutes,
                Notes = dto.Notes,
                // Audit fields would typically be set by your service layer
                RowCreatedOn = DateTime.UtcNow,
                RowCreatedBy = "System" // or get from current user context
            };
        }

        public SessionDto MapToDto(Session entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SessionDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                PracticeDate = entity.PracticeDate,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Activity = entity.Activity,
                DurationMinutes = entity.DurationMinutes,
                Notes = entity.Notes,
                RowCreatedOn = entity.RowCreatedOn,
                RowCreatedBy = entity.RowCreatedBy,
                RowModifiedOn = entity.RowModifiedOn,
                RowModifiedBy = entity.RowModifiedBy
            };
        }
    }
}
