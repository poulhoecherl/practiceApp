using System;
using System.Collections.Generic;
using System.Linq;
using Practice.Data.Models;
using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface IMappingService
    {
        // Drill mappings
        DrillDto MapToDto(Drill entity);
        DrillDto MapToDrillDto(Drill entity);
        
        // Session mappings
        SessionDto MapToDto(Session entity);
        Session MapToEntity(SessionDto dto);
        
        // Song mappings
        SongDto MapToDto(Song entity);
        Song MapToEntity(SongDto dto);

        // Generic collection helpers
        IEnumerable<TDto> MapToDto<TDto, TEntity>(IEnumerable<TEntity> entities, Func<TEntity, TDto> mapFunction);
        IEnumerable<TEntity> MapToEntity<TDto, TEntity>(IEnumerable<TDto> dtos, Func<TDto, TEntity> mapFunction);
        
        // LINQ projection helpers for efficient querying
        IQueryable<DrillDto> ProjectToDrillDto(IQueryable<Drill> query);
        IQueryable<SessionDto> ProjectToSessionDto(IQueryable<Session> query);
        
    }
}
