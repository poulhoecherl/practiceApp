using Practice.Data.Models;

namespace Practice.Data.Interfaces;

public interface IPracticeService
{
    // Session management
    Task<Session> StartSessionAsync();
    Task<Session> EndSessionAsync(int sessionId);
    Task<IEnumerable<Session>> GetRecentSessionsAsync(int count = 10);
    
    // Drill management
    Task<Drill> CreateDrillAsync(int drillsId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<Drill>> GetDrillsForSessionAsync(int sessionId);
    Task<Drill> CompleteDrillAsync(int drillId);
    
    // Song management
    Task<Song> AddSongToCollectionAsync(int songsId, Song song);
    Task<IEnumerable<Song>> GetSongsFromCollectionAsync(int songsId);
    Task<Song> RemoveSongFromCollectionAsync(int songId);
    
    // Statistics and reporting
    Task<TimeSpan> GetTotalPracticeTimeAsync();
    Task<int> GetTotalDrillsCompletedAsync();
    Task<int> GetTotalSongsLearnedAsync();
    Task<Dictionary<DateTime, TimeSpan>> GetPracticeTimeByDateAsync(DateTime startDate, DateTime endDate);
}
