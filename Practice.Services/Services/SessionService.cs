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

        public async Task<bool> DeleteSessionAsync(int id)
        {
            var result = await _unitOfWork.Sessions.DeleteAsync(id);
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
    }
}
