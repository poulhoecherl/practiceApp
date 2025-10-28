using Practice.Data.Models;
using Practice.Services.DTOs;

namespace Practice.Services.Mapping
{
    public class Sesssion
    {
        public Session MapToEntity(SessionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Session
            {
                UserId = dto.UserId,
                PracticeDate = dto.PracticeDate,
                
                Activity = dto.Activity,
                
                Notes = dto.Notes
            };
        }

        public SessionDto MapToDto(Session entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SessionDto()
            {
                UserId = entity.UserId,
                PracticeDate = entity.PracticeDate,
                Activity = entity.Activity,
                Notes = entity.Notes
            };


        }
    }
}
