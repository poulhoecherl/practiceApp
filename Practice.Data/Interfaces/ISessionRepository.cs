using Practice.Data.Models;

namespace Practice.Data.Interfaces;

public interface ISessionRepository : IRepository<Session>
{
    // Session-specific methods
    Task<IEnumerable<Session>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Session>> GetActiveSessionsAsync();
    Task<IEnumerable<Session>> GetCompletedSessionsAsync();
    Task<Session?> GetCurrentSessionAsync();
    Task<Session?> GetLatestSessionAsync();
    Task<TimeSpan> GetTotalSessionDurationAsync();
    Task<TimeSpan> GetSessionDurationAsync(int sessionId);
    Task<IEnumerable<Session>> GetSessionsForTodayAsync();
}
