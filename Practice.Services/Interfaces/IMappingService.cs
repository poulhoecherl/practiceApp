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
        DrillResponseDto MapToDto(Drill entity);
        DrillListDto MapToDrillListDto(Drill entity);
        DrillSearchDto MapToDrillSearchDto(Drill entity);
        DrillSummaryDto MapToDrillSummaryDto(Drill entity);
        Drill MapToEntity(CreateDrillDto dto);
        void MapToEntity(UpdateDrillDto dto, Drill entity);
        
        // Drills mappings
        DrillsResponseDto MapToDto(Drills entity);
        DrillsListDto MapToDrillsListDto(Drills entity);
        DrillsSummaryDto MapToDrillsSummaryDto(Drills entity);
        Drills MapToEntity(CreateDrillsDto dto);
        void MapToEntity(UpdateDrillsDto dto, Drills entity);
        
        // Session mappings
        SessionResponseDto MapToDto(Session entity);
        SessionListDto MapToSessionListDto(Session entity);
        SessionSummaryDto MapToSessionSummaryDto(Session entity);
        Session MapToEntity(CreateSessionDto dto);
        void MapToEntity(UpdateSessionDto dto, Session entity);
        
        
        
        // Generic collection helpers
        IEnumerable<TDto> MapToDto<TDto, TEntity>(IEnumerable<TEntity> entities, Func<TEntity, TDto> mapFunction);
        IEnumerable<TEntity> MapToEntity<TDto, TEntity>(IEnumerable<TDto> dtos, Func<TDto, TEntity> mapFunction);
        
        // LINQ projection helpers for efficient querying
        IQueryable<DrillResponseDto> ProjectToDrillResponseDto(IQueryable<Drill> query);
        IQueryable<DrillListDto> ProjectToDrillListDto(IQueryable<Drill> query);
        IQueryable<SessionResponseDto> ProjectToSessionResponseDto(IQueryable<Session> query);
        IQueryable<SessionListDto> ProjectToSessionListDto(IQueryable<Session> query);
        
    }
}
