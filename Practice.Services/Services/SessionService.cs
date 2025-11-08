using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Interfaces;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;

        public SessionService(IUnitOfWork unitOfWork, IMappingService mappingService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        // Session CRUD operations
        public async Task<SessionDto> GetSessionAsync(int id)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id);
            if (session == null)
                throw new KeyNotFoundException($"Session with id {id} not found");

            return _mappingService.MapToDto(session);
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessionsAsync()
        {
            var sessions = await _unitOfWork.Sessions.GetAllAsync();
            return _mappingService.MapToDto(sessions, _mappingService.MapToDto);
        }

        public async Task<SessionDto> CreateSessionAsync(SessionDto createSessionDto)
        {
            if (createSessionDto == null)
                throw new ArgumentNullException(nameof(createSessionDto));

            var session = _mappingService.MapToEntity(createSessionDto);
            var createdSession = await _unitOfWork.Sessions.CreateAsync(session);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(createdSession);
        }

        public async Task<SessionDto> UpdateSessionAsync(int id, SessionDto updateSessionDto)
        {
            if (updateSessionDto == null)
                throw new ArgumentNullException(nameof(updateSessionDto));

            var existingSession = await _unitOfWork.Sessions.GetByIdAsync(id);
            if (existingSession == null)
                throw new KeyNotFoundException($"Session with id {id} not found");

            var updatedSession = await _unitOfWork.Sessions.UpdateAsync(existingSession);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(updatedSession);
        }

        /// <summary>
        /// SOFT DELETE ONLY - Marks session as deleted but preserves data
        /// </summary>
        public async Task<bool> DeleteSessionAsync(int id)
        {
            var result = await _unitOfWork.Sessions.DeleteAsync(id); // This should trigger soft delete
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        // Session-specific operations
        /// <summary>
        /// Creates a new practice session that starts immediately and is open-ended.
        /// This is the main method for starting a new practice session.
        /// </summary>
        /// <returns>The newly created session DTO</returns>
        public async Task<SessionDto> CreateNewSessionAsync()
        {
            var now = DateTime.UtcNow;
            
            // Check if there's already an active session
            var currentSession = await _unitOfWork.Sessions.GetCurrentSessionAsync();
            if (currentSession != null)
            {
                throw new InvalidOperationException("Cannot create a new session while another session is active. Please end the current session first.");
            }

            // Create a new session DTO with current time as start and a future placeholder end time
            var createSessionDto = new SessionDto
            {
                StartTime = now,
                EndTime = now.AddHours(1) // Default 1-hour session, can be updated later
            };

            // Use the standard create method which internally uses MappingService
            var newSession = await CreateSessionAsync(createSessionDto);

            return newSession;
        }

        /// <summary>
        /// Alternative method that starts a new session with only a start time.
        /// The session remains "active" until explicitly ended.
        /// </summary>
        /// <returns>The newly created active session DTO</returns>
        public async Task<SessionDto> StartNewSessionAsync()
        {
            var now = DateTime.UtcNow;
            
            // Check if there's already an active session
            var activeSession = await GetCurrentSessionAsync();
            if (activeSession != null)
            {
                throw new InvalidOperationException($"Session {activeSession.Id} is already active. End it before starting a new one.");
            }

            // Create a new session entity directly for more control
            var session = new Session
            {
                PracticeDate = now,
                
            };

            var createdSession = await _unitOfWork.Sessions.CreateAsync(session);
            await _unitOfWork.SaveChangesAsync();

            // Map to DTO using MappingService
            return _mappingService.MapToDto(createdSession);
        }

        /// <summary>
        /// Ends the current active session by setting the end time to now.
        /// </summary>
        /// <returns>The updated session DTO</returns>
        public async Task<SessionDto> EndCurrentSessionAsync()
        {
            var currentSession = await _unitOfWork.Sessions.GetCurrentSessionAsync();
            if (currentSession == null)
            {
                throw new InvalidOperationException("No active session found to end.");
            }

            // Update the end time to now
            
            var updatedSession = await _unitOfWork.Sessions.UpdateAsync(currentSession);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(updatedSession);
        }

        public async Task<SessionDto?> GetCurrentSessionAsync()
        {
            var currentSession = await _unitOfWork.Sessions.GetCurrentSessionAsync();
            return currentSession != null ? _mappingService.MapToDto(currentSession) : null;
        }

        public async Task<SessionDto?> GetLatestSessionAsync()
        {
            var latestSession = await _unitOfWork.Sessions.GetLatestSessionAsync();
            return latestSession != null ? _mappingService.MapToDto(latestSession) : null;
        }

        // Session query operations
        public async Task<IEnumerable<SessionDto>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sessions = await _unitOfWork.Sessions.GetSessionsByDateRangeAsync(startDate, endDate);
            return _mappingService.MapToDto(sessions, _mappingService.MapToDto);
        }

        public async Task<IEnumerable<SessionDto>> GetActiveSessionsAsync()
        {
            var sessions = await _unitOfWork.Sessions.GetActiveSessionsAsync();
            return _mappingService.MapToDto(sessions, _mappingService.MapToDto);
        }

        public async Task<IEnumerable<SessionDto>> GetCompletedSessionsAsync()
        {
            var sessions = await _unitOfWork.Sessions.GetCompletedSessionsAsync();
            return _mappingService.MapToDto(sessions, _mappingService.MapToDto);
        }

        public async Task<IEnumerable<SessionDto>> GetSessionsForTodayAsync()
        {
            var sessions = await _unitOfWork.Sessions.GetSessionsForTodayAsync();
            return _mappingService.MapToDto(sessions, _mappingService.MapToDto);
        }

        // Session summary and statistics
        public async Task<SessionDto> GetSessionSummaryAsync(int id)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(id);
            if (session == null)
                throw new KeyNotFoundException($"Session with id {id} not found");

            return _mappingService.MapToDto(session);
        }

        public async Task<TimeSpan> GetTotalSessionDurationAsync()
        {
            return await _unitOfWork.Sessions.GetTotalSessionDurationAsync();
        }

        public async Task<TimeSpan> GetSessionDurationAsync(int sessionId)
        {
            return await _unitOfWork.Sessions.GetSessionDurationAsync(sessionId);
        }

        public async Task<bool> SessionExistsAsync(int id)
        {
            return await _unitOfWork.Sessions.ExistsAsync(id);
        }

        public async Task<int> GetSessionCountAsync()
        {
            return await _unitOfWork.Sessions.CountAsync();
        }

        /// <summary>
        /// SOFT DELETE AWARE: Adds a drill to session or restores if previously deleted
        /// </summary>
        public async Task<bool> AddDrillToSessionAsync(int sessionId, int drillId, string? notes = null, int? rating = null)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId);
            if (session == null || session.Deleted)
                throw new KeyNotFoundException($"Active session with id {sessionId} not found");

            var drill = await _unitOfWork.Drills.GetByIdAsync(drillId);
            if (drill == null || drill.Deleted)
                throw new KeyNotFoundException($"Active drill with id {drillId} not found");

            using var context = new PracticeDbContext();
            
            var existingRelation = await context.SessionDrills
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(sd => sd.SessionId == sessionId && sd.DrillId == drillId);
            
            if (existingRelation != null)
            {
                if (existingRelation.Deleted)
                {
                    // RESTORE the soft-deleted relationship
                    existingRelation.Deleted = false;
                    existingRelation.CompletedAt = DateTime.UtcNow;
                    existingRelation.Notes = notes;
                    existingRelation.PerformanceRating = rating;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false; // Already exists and is active
            }
            
            var sessionDrill = new Session_Drill
            {
                SessionId = sessionId,
                DrillId = drillId,
                CompletedAt = DateTime.UtcNow,
                Notes = notes,
                PerformanceRating = rating
            };
            
            await context.SessionDrills.AddAsync(sessionDrill);
            await context.SaveChangesAsync();
            
            return true;
        }

        /// <summary>
        /// SOFT DELETE AWARE: Adds a song to session or restores if previously deleted
        /// </summary>
        public async Task<bool> AddSongToSessionAsync(int sessionId, int songId, string? notes = null)
        {
            using var context = new PracticeDbContext();
            
            var existingRelation = await context.SessionSongs
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(ss => ss.SessionId == sessionId && ss.SongId == songId);
            
            if (existingRelation != null)
            {
                if (existingRelation.Deleted)
                {
                    // RESTORE the soft-deleted relationship
                    existingRelation.Deleted = false;
                    existingRelation.CompletedAt = DateTime.UtcNow;
                    existingRelation.Notes = notes;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            
            var sessionSong = new Session_Song
            {
                SessionId = sessionId,
                SongId = songId,
                CompletedAt = DateTime.UtcNow,
                Notes = notes
            };
            
            await context.SessionSongs.AddAsync(sessionSong);
            await context.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<Drill>> GetSessionDrillsAsync(int sessionId)
        {
            using var context = new PracticeDbContext();
            
            var drills = await context.SessionDrills
                .Where(sd => sd.SessionId == sessionId) // Query filter automatically excludes deleted
                .Include(sd => sd.Drill)
                .Select(sd => sd.Drill)
                .ToListAsync();
            
            return drills;
        }

        public async Task<IEnumerable<Song>> GetSessionSongsAsync(int sessionId)
        {
            using var context = new PracticeDbContext();
            
            var songs = await context.SessionSongs
                .Where(ss => ss.SessionId == sessionId) // Query filter automatically excludes deleted
                .Include(ss => ss.Song)
                .Select(ss => ss.Song)
                .ToListAsync();
            
            return songs;
        }

        /// <summary>
        /// SOFT DELETE: Marks drill relationship as deleted but preserves data
        /// </summary>
        public async Task<bool> RemoveDrillFromSessionAsync(int sessionId, int drillId)
        {
            using var context = new PracticeDbContext();
            
            var sessionDrill = await context.SessionDrills
                .FirstOrDefaultAsync(sd => sd.SessionId == sessionId && sd.DrillId == drillId);
            
            if (sessionDrill == null)
                return false;
            
            // SOFT DELETE: Mark as deleted instead of removing
            sessionDrill.Deleted = true;
            await context.SaveChangesAsync();
            
            return true;
        }

        /// <summary>
        /// SOFT DELETE: Marks song relationship as deleted but preserves data
        /// </summary>
        public async Task<bool> RemoveSongFromSessionAsync(int sessionId, int songId)
        {
            using var context = new PracticeDbContext();
            
            var sessionSong = await context.SessionSongs
                .FirstOrDefaultAsync(ss => ss.SessionId == sessionId && ss.SongId == songId);
            
            if (sessionSong == null)
                return false;
            
            // SOFT DELETE: Mark as deleted instead of removing
            sessionSong.Deleted = true;
            await context.SaveChangesAsync();
            
            return true;
        }

        /// <summary>
        /// AUDIT FUNCTION: Gets complete history including deleted relationships
        /// </summary>
        public async Task<IEnumerable<Session_Drill>> GetSessionDrillHistoryAsync(int sessionId)
        {
            using var context = new PracticeDbContext();
            
            return await context.SessionDrills
                .IgnoreQueryFilters() // Include deleted records for audit
                .Where(sd => sd.SessionId == sessionId)
                .Include(sd => sd.Drill)
                .ToListAsync();
        }

        /// <summary>
        /// ADMIN FUNCTION: Restore a soft-deleted drill relationship
        /// </summary>
        public async Task<bool> RestoreDrillToSessionAsync(int sessionId, int drillId)
        {
            using var context = new PracticeDbContext();
            
            var sessionDrill = await context.SessionDrills
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(sd => sd.SessionId == sessionId && sd.DrillId == drillId && sd.Deleted);
            
            if (sessionDrill == null)
                return false;
            
            sessionDrill.Deleted = false;
            await context.SaveChangesAsync();
            
            return true;
        }

        /// <summary>
        /// ADMIN FUNCTION: Restore a soft-deleted song relationship
        /// </summary>
        public async Task<bool> RestoreSongToSessionAsync(int sessionId, int songId)
        {
            using var context = new PracticeDbContext();
            
            var sessionSong = await context.SessionSongs
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(ss => ss.SessionId == sessionId && ss.SongId == songId && ss.Deleted);
            
            if (sessionSong == null)
                return false;
            
            sessionSong.Deleted = false;
            await context.SaveChangesAsync();
            
            return true;
        }
    }
}
