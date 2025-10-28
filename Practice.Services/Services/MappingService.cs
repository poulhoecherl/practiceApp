using System;
using System.Collections.Generic;
using System.Linq;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Services
{
    public class MappingService : IMappingService
    {
        // Drill mappings
        public DrillResponseDto MapToDto(Drill entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillResponseDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }
        
        public DrillListDto MapToDrillListDto(Drill entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillListDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }
        
        public DrillSearchDto MapToDrillSearchDto(Drill entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillSearchDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }
        
        public DrillSummaryDto MapToDrillSummaryDto(Drill entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillSummaryDto
            {
                Id = entity.Id,
                TotalDurationMinutes = (int)(entity.EndDate - entity.StartDate).TotalMinutes
            };
        }

        public Drill MapToEntity(CreateDrillDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Drill
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
            };
        }

        public void MapToEntity(UpdateDrillDto dto, Drill entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
        }
        
        // Drills mappings
        public DrillsResponseDto MapToDto(Drills entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillsResponseDto
            {
                Id = entity.Id,
                DrillCollection = entity.DrillCollection.Select(MapToDrillListDto).ToList()
            };
        }

        public DrillsListDto MapToDrillsListDto(Drills entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillsListDto
            {
                Id = entity.Id,
                Count = entity.DrillCollection.Count
            };
        }

        public DrillsSummaryDto MapToDrillsSummaryDto(Drills entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DrillsSummaryDto
            {
                Id = entity.Id,
                TotalDrills = entity.DrillCollection.Count,
                TotalDurationMinutes = entity.DrillCollection.Sum(d => (int)(d.EndDate - d.StartDate).TotalMinutes)
            };
        }

        public Drills MapToEntity(CreateDrillsDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Drills
            {
                DrillCollection = dto.DrillCollection?.Select(MapToEntity).ToList() ?? new List<Drill>()
            };
        }

        public void MapToEntity(UpdateDrillsDto dto, Drills entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.DrillCollection = dto.DrillCollection?.Select(MapToEntity).ToList() ?? new List<Drill>();
        }

        // Session mappings
        public SessionResponseDto MapToDto(Session entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SessionResponseDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }

        public SessionListDto MapToSessionListDto(Session entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SessionListDto
            {
                Id = entity.Id,
                
            };
        }

        public SessionSummaryDto MapToSessionSummaryDto(Session entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SessionSummaryDto
            {
                Id = entity.Id,
                
            };
        }

        public Session MapToEntity(CreateSessionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Session
            {
                
            };
        }

        public void MapToEntity(UpdateSessionDto dto, Session entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            
        }

        // Song mappings
        public SongResponseDto MapToDto(Song entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongResponseDto
            {
                Id = entity.Id,
                SongsId = entity.SongsId
            };
        }

        public SongListDto MapToSongListDto(Song entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongListDto
            {
                Id = entity.Id,
                SongsId = entity.SongsId
            };
        }

        public SongSearchDto MapToSongSearchDto(Song entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongSearchDto
            {
                Id = entity.Id,
                SongsId = entity.SongsId
            };
        }

        public SongSummaryDto MapToSongSummaryDto(Song entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongSummaryDto
            {
                Id = entity.Id
            };
        }

        public Song MapToEntity(CreateSongDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Song
            {
                SongsId = dto.SongsId,
            };
        }

        public void MapToEntity(UpdateSongDto dto, Song entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.SongsId = dto.SongsId;
        }

        // Songs mappings
        public SongsResponseDto MapToDto(Songs entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongsResponseDto
            {
                Id = entity.Id,
                SongCollection = entity.SongCollection.Select(MapToSongListDto).ToList()
            };
        }

        public SongsListDto MapToSongsListDto(Songs entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongsListDto
            {
                Id = entity.Id,
                Count = entity.SongCollection.Count
            };
        }

        public SongsSummaryDto MapToSongsSummaryDto(Songs entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new SongsSummaryDto
            {
                Id = entity.Id,
                TotalSongs = entity.SongCollection.Count,
                TotalDurationMinutes = 0 // Assuming duration calculation is done elsewhere
            };
        }

        public Songs MapToEntity(CreateSongsDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Songs
            {
                SongCollection = dto.SongCollection?.Select(MapToEntity).ToList() ?? new List<Song>()
            };
        }

        public void MapToEntity(UpdateSongsDto dto, Songs entity)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.SongCollection = dto.SongCollection?.Select(MapToEntity).ToList() ?? new List<Song>();
        }

        // Generic collection helpers
        public IEnumerable<TDto> MapToDto<TDto, TEntity>(IEnumerable<TEntity> entities, Func<TEntity, TDto> mapFunction)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            if (mapFunction == null) throw new ArgumentNullException(nameof(mapFunction));

            return entities.Select(mapFunction).ToList();
        }

        public IEnumerable<TEntity> MapToEntity<TDto, TEntity>(IEnumerable<TDto> dtos, Func<TDto, TEntity> mapFunction)
        {
            if (dtos == null) throw new ArgumentNullException(nameof(dtos));
            if (mapFunction == null) throw new ArgumentNullException(nameof(mapFunction));

            return dtos.Select(mapFunction).ToList();
        }

        // LINQ projection helpers
        public IQueryable<DrillResponseDto> ProjectToDrillResponseDto(IQueryable<Drill> query)
        {
            return query.Select(entity => new DrillResponseDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            });
        }

        public IQueryable<DrillListDto> ProjectToDrillListDto(IQueryable<Drill> query)
        {
            return query.Select(entity => new DrillListDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            });
        }

        public IQueryable<SessionResponseDto> ProjectToSessionResponseDto(IQueryable<Session> query)
        {
            return query.Select(entity => new SessionResponseDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            });
        }

        public IQueryable<SessionListDto> ProjectToSessionListDto(IQueryable<Session> query)
        {
            return query.Select(entity => new SessionListDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate
            });
        }

        public IQueryable<SongResponseDto> ProjectToSongResponseDto(IQueryable<Song> query)
        {
            return query.Select(entity => new SongResponseDto
            {
                Id = entity.Id,
                SongsId = entity.SongsId
            });
        }

        public IQueryable<SongListDto> ProjectToSongListDto(IQueryable<Song> query)
        {
            return query.Select(entity => new SongListDto
            {
                Id = entity.Id,
                SongsId = entity.SongsId
            });
        }
    }
}
