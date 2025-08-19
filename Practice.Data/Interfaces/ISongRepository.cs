using Practice.Data.Models;

namespace Practice.Data.Interfaces;

public interface ISongRepository : IRepository<Song>
{
    // Song-specific methods
    Task<IEnumerable<Song>> GetSongsBySongsIdAsync(int songsId);
    Task<Song?> GetSongWithSongsAsync(int id);
    Task<IEnumerable<Song>> SearchSongsByTitleAsync(string title);
    Task<IEnumerable<Song>> GetFavoriteSongsAsync();
    Task<IEnumerable<Song>> GetRecentSongsAsync(int count = 10);
}
