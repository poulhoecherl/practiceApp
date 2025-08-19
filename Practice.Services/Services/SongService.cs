using Practice.Data.Interfaces;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Services
{
    public class SongService : ISongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;

        public SongService(IUnitOfWork unitOfWork, IMappingService mappingService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        // Song CRUD operations
        public async Task<SongResponseDto> GetSongAsync(int id)
        {
            var song = await _unitOfWork.Songs.GetByIdAsync(id);
            if (song == null)
                throw new KeyNotFoundException($"Song with id {id} not found");

            return _mappingService.MapToDto(song);
        }

        public async Task<IEnumerable<SongListDto>> GetAllSongsAsync()
        {
            var songs = await _unitOfWork.Songs.GetAllAsync();
            return _mappingService.MapToDto(songs, _mappingService.MapToSongListDto);
        }

        public async Task<IEnumerable<SongListDto>> GetSongsBySongsIdAsync(int songsId)
        {
            var songs = await _unitOfWork.Songs.GetSongsBySongsIdAsync(songsId);
            return _mappingService.MapToDto(songs, _mappingService.MapToSongListDto);
        }

        public async Task<SongResponseDto> GetSongWithSongsAsync(int id)
        {
            var song = await _unitOfWork.Songs.GetSongWithSongsAsync(id);
            if (song == null)
                throw new KeyNotFoundException($"Song with id {id} not found");

            return _mappingService.MapToDto(song);
        }

        public async Task<SongResponseDto> CreateSongAsync(CreateSongDto createSongDto)
        {
            if (createSongDto == null)
                throw new ArgumentNullException(nameof(createSongDto));

            var song = _mappingService.MapToEntity(createSongDto);
            var createdSong = await _unitOfWork.Songs.CreateAsync(song);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(createdSong);
        }

        public async Task<SongResponseDto> UpdateSongAsync(int id, UpdateSongDto updateSongDto)
        {
            if (updateSongDto == null)
                throw new ArgumentNullException(nameof(updateSongDto));

            var existingSong = await _unitOfWork.Songs.GetByIdAsync(id);
            if (existingSong == null)
                throw new KeyNotFoundException($"Song with id {id} not found");

            _mappingService.MapToEntity(updateSongDto, existingSong);
            var updatedSong = await _unitOfWork.Songs.UpdateAsync(existingSong);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(updatedSong);
        }

        public async Task<bool> DeleteSongAsync(int id)
        {
            var result = await _unitOfWork.Songs.DeleteAsync(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        // Song search and query operations
        public async Task<IEnumerable<SongSearchDto>> SearchSongsByTitleAsync(string title)
        {
            var songs = await _unitOfWork.Songs.SearchSongsByTitleAsync(title);
            return _mappingService.MapToDto(songs, _mappingService.MapToSongSearchDto);
        }

        public async Task<IEnumerable<SongListDto>> GetFavoriteSongsAsync()
        {
            var songs = await _unitOfWork.Songs.GetFavoriteSongsAsync();
            return _mappingService.MapToDto(songs, _mappingService.MapToSongListDto);
        }

        public async Task<IEnumerable<SongListDto>> GetRecentSongsAsync(int count = 10)
        {
            var songs = await _unitOfWork.Songs.GetRecentSongsAsync(count);
            return _mappingService.MapToDto(songs, _mappingService.MapToSongListDto);
        }

        // Song summary operations
        public async Task<SongSummaryDto> GetSongSummaryAsync(int id)
        {
            var song = await _unitOfWork.Songs.GetByIdAsync(id);
            if (song == null)
                throw new KeyNotFoundException($"Song with id {id} not found");

            return _mappingService.MapToSongSummaryDto(song);
        }

        public async Task<bool> SongExistsAsync(int id)
        {
            return await _unitOfWork.Songs.ExistsAsync(id);
        }

        public async Task<int> GetSongCountAsync()
        {
            return await _unitOfWork.Songs.CountAsync();
        }
    }
}
