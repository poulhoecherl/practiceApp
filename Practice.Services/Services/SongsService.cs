using Practice.Data.Interfaces;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Services
{
    public class SongsService : ISongsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;

        public SongsService(IUnitOfWork unitOfWork, IMappingService mappingService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        // Songs CRUD operations
        public async Task<SongsResponseDto> GetSongsAsync(int id)
        {
            var songs = await _unitOfWork.SongsCollections.GetByIdAsync(id);
            if (songs == null)
                throw new KeyNotFoundException($"Songs collection with id {id} not found");

            return _mappingService.MapToDto(songs);
        }

        public async Task<IEnumerable<SongsListDto>> GetAllSongsAsync()
        {
            var songsCollections = await _unitOfWork.SongsCollections.GetAllAsync();
            return _mappingService.MapToDto(songsCollections, _mappingService.MapToSongsListDto);
        }

        public async Task<SongsResponseDto> GetSongsWithSongCollectionAsync(int id)
        {
            var songs = await _unitOfWork.SongsCollections.GetSongsWithSongCollectionAsync(id);
            if (songs == null)
                throw new KeyNotFoundException($"Songs collection with id {id} not found");

            return _mappingService.MapToDto(songs);
        }

        public async Task<SongsResponseDto> CreateSongsAsync(CreateSongsDto createSongsDto)
        {
            if (createSongsDto == null)
                throw new ArgumentNullException(nameof(createSongsDto));

            var songs = _mappingService.MapToEntity(createSongsDto);
            var createdSongs = await _unitOfWork.SongsCollections.CreateAsync(songs);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(createdSongs);
        }

        public async Task<SongsResponseDto> UpdateSongsAsync(int id, UpdateSongsDto updateSongsDto)
        {
            if (updateSongsDto == null)
                throw new ArgumentNullException(nameof(updateSongsDto));

            var existingSongs = await _unitOfWork.SongsCollections.GetByIdAsync(id);
            if (existingSongs == null)
                throw new KeyNotFoundException($"Songs collection with id {id} not found");

            _mappingService.MapToEntity(updateSongsDto, existingSongs);
            var updatedSongs = await _unitOfWork.SongsCollections.UpdateAsync(existingSongs);
            await _unitOfWork.SaveChangesAsync();

            return _mappingService.MapToDto(updatedSongs);
        }

        public async Task<bool> DeleteSongsAsync(int id)
        {
            var result = await _unitOfWork.SongsCollections.DeleteAsync(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        // Songs query operations
        public async Task<IEnumerable<SongsResponseDto>> GetSongsWithAllSongsAsync()
        {
            var songsCollections = await _unitOfWork.SongsCollections.GetSongsWithAllSongsAsync();
            return _mappingService.MapToDto(songsCollections, _mappingService.MapToDto);
        }

        public async Task<IEnumerable<SongsResponseDto>> GetSongsWithSongCountAsync(int minimumSongCount)
        {
            var songsCollections = await _unitOfWork.SongsCollections.GetSongsWithSongCountAsync(minimumSongCount);
            return _mappingService.MapToDto(songsCollections, _mappingService.MapToDto);
        }

        public async Task<IEnumerable<SongsResponseDto>> GetPopularSongsCollectionsAsync()
        {
            var songsCollections = await _unitOfWork.SongsCollections.GetPopularSongsCollectionsAsync();
            return _mappingService.MapToDto(songsCollections, _mappingService.MapToDto);
        }

        // Songs summary operations
        public async Task<SongsSummaryDto> GetSongsSummaryAsync(int id)
        {
            var songs = await _unitOfWork.SongsCollections.GetByIdAsync(id);
            if (songs == null)
                throw new KeyNotFoundException($"Songs collection with id {id} not found");

            return _mappingService.MapToSongsSummaryDto(songs);
        }

        public async Task<bool> SongsExistsAsync(int id)
        {
            return await _unitOfWork.SongsCollections.ExistsAsync(id);
        }

        public async Task<bool> HasSongsAsync(int songsId)
        {
            return await _unitOfWork.SongsCollections.HasSongsAsync(songsId);
        }

        public async Task<int> GetSongCountAsync(int songsId)
        {
            return await _unitOfWork.SongsCollections.GetSongCountAsync(songsId);
        }

        public async Task<int> GetSongsCollectionCountAsync()
        {
            return await _unitOfWork.SongsCollections.CountAsync();
        }
    }
}
