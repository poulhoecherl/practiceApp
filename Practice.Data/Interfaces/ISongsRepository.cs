using Practice.Data.Models;

namespace Practice.Data.Interfaces;

public interface ISongsRepository : IRepository<Songs>
{
    // Songs-specific methods
    Task<Songs?> GetSongsWithSongCollectionAsync(int id);
    Task<IEnumerable<Songs>> GetSongsWithAllSongsAsync();
    Task<IEnumerable<Songs>> GetSongsWithSongCountAsync(int minimumSongCount);
    Task<bool> HasSongsAsync(int songsId);
    Task<int> GetSongCountAsync(int songsId);
    Task<IEnumerable<Songs>> GetPopularSongsCollectionsAsync();
}
