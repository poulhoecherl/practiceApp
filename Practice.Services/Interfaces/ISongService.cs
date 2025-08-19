using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface ISongService
    {
        // Song CRUD operations
        Task<SongResponseDto> GetSongAsync(int id);
        Task<IEnumerable<SongListDto>> GetAllSongsAsync();
        Task<IEnumerable<SongListDto>> GetSongsBySongsIdAsync(int songsId);
        Task<SongResponseDto> GetSongWithSongsAsync(int id);
        Task<SongResponseDto> CreateSongAsync(CreateSongDto createSongDto);
        Task<SongResponseDto> UpdateSongAsync(int id, UpdateSongDto updateSongDto);
        Task<bool> DeleteSongAsync(int id);
        
        // Song search and query operations
        Task<IEnumerable<SongSearchDto>> SearchSongsByTitleAsync(string title);
        Task<IEnumerable<SongListDto>> GetFavoriteSongsAsync();
        Task<IEnumerable<SongListDto>> GetRecentSongsAsync(int count = 10);
        
        // Song summary operations
        Task<SongSummaryDto> GetSongSummaryAsync(int id);
        Task<bool> SongExistsAsync(int id);
        Task<int> GetSongCountAsync();
    }
}
