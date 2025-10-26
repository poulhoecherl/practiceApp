using Practice.Data.Models;
using Practice.Data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                StartDate = dto.PracticeDate,
                EndDate = dto.PracticeDate.AddMinutes(dto.Duration), // Assuming duration is practice time
                Activity = dto.Activity,
                Duration = dto.Duration,
                Notes = dto.Notes
            };
        }

        public SessionDto MapToDto(Session entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SessionDto()
            {
                UserId = entity.UserId,
                PracticeDate = entity.StartDate,
                Activity = entity.Activity,
                Duration = entity.Duration,
                Notes = entity.Notes
            };


        }
    }
}
