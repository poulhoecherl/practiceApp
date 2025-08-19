using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface ISessionService
    {
        // Session CRUD operations
        Task<SessionResponseDto> GetSessionAsync(int id);
        Task<IEnumerable<SessionListDto>> GetAllSessionsAsync();
        Task<SessionResponseDto> CreateSessionAsync(CreateSessionDto createSessionDto);
        Task<SessionResponseDto> UpdateSessionAsync(int id, UpdateSessionDto updateSessionDto);
        Task<bool> DeleteSessionAsync(int id);
        
        // Session-specific operations
        Task<SessionResponseDto> CreateNewSessionAsync();
        Task<SessionResponseDto> StartNewSessionAsync();
        Task<SessionResponseDto> EndCurrentSessionAsync();
        Task<SessionResponseDto?> GetCurrentSessionAsync();
        Task<SessionResponseDto?> GetLatestSessionAsync();
        
        // Session query operations
        Task<IEnumerable<SessionResponseDto>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<SessionResponseDto>> GetActiveSessionsAsync();
        Task<IEnumerable<SessionResponseDto>> GetCompletedSessionsAsync();
        Task<IEnumerable<SessionResponseDto>> GetSessionsForTodayAsync();
        
        // Session summary and statistics
        Task<SessionSummaryDto> GetSessionSummaryAsync(int id);
        Task<TimeSpan> GetTotalSessionDurationAsync();
        Task<TimeSpan> GetSessionDurationAsync(int sessionId);
        Task<bool> SessionExistsAsync(int id);
        Task<int> GetSessionCountAsync();
    }
}
