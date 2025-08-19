using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface ISongsService
    {
        // Songs CRUD operations
        Task<SongsResponseDto> GetSongsAsync(int id);
        Task<IEnumerable<SongsListDto>> GetAllSongsAsync();
        Task<SongsResponseDto> GetSongsWithSongCollectionAsync(int id);
        Task<SongsResponseDto> CreateSongsAsync(CreateSongsDto createSongsDto);
        Task<SongsResponseDto> UpdateSongsAsync(int id, UpdateSongsDto updateSongsDto);
        Task<bool> DeleteSongsAsync(int id);
        
        // Songs query operations
        Task<IEnumerable<SongsResponseDto>> GetSongsWithAllSongsAsync();
        Task<IEnumerable<SongsResponseDto>> GetSongsWithSongCountAsync(int minimumSongCount);
        Task<IEnumerable<SongsResponseDto>> GetPopularSongsCollectionsAsync();
        
        // Songs summary operations
        Task<SongsSummaryDto> GetSongsSummaryAsync(int id);
        Task<bool> SongsExistsAsync(int id);
        Task<bool> HasSongsAsync(int songsId);
        Task<int> GetSongCountAsync(int songsId);
        Task<int> GetSongsCollectionCountAsync();
    }
}
