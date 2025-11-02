using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface ISessionService
    {
        // Session CRUD operations
        Task<SessionDto> GetSessionAsync(int id);
        Task<IEnumerable<SessionDto>> GetAllSessionsAsync();
        Task<SessionDto> CreateSessionAsync(SessionDto createSessionDto);
        Task<SessionDto> UpdateSessionAsync(int id, SessionDto updateSessionDto);
        Task<bool> DeleteSessionAsync(int id);
        
        // Session-specific operations
        Task<SessionDto> CreateNewSessionAsync();
        Task<SessionDto> StartNewSessionAsync();
        Task<SessionDto> EndCurrentSessionAsync();
        Task<SessionDto?> GetCurrentSessionAsync();
        Task<SessionDto?> GetLatestSessionAsync();
        
        // Session query operations
        Task<IEnumerable<SessionDto>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SessionDto>> GetActiveSessionsAsync();
        Task<IEnumerable<SessionDto>> GetCompletedSessionsAsync();
        Task<IEnumerable<SessionDto>> GetSessionsForTodayAsync();
        
        // Session summary and statistics
        Task<SessionDto> GetSessionSummaryAsync(int id);
        Task<TimeSpan> GetTotalSessionDurationAsync();
        Task<TimeSpan> GetSessionDurationAsync(int sessionId);
        Task<bool> SessionExistsAsync(int id);
        Task<int> GetSessionCountAsync();
    }
}
