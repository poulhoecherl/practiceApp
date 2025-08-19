using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Examples
{
    /// <summary>
    /// Example class demonstrating how to consume SongService and SongsService
    /// in business logic scenarios using the proper DTOs
    /// </summary>
    public class SongServiceUsageExample
    {
        private readonly ISongService _songService;
        private readonly ISongsService _songsService;

        public SongServiceUsageExample(ISongService songService, ISongsService songsService)
        {
            _songService = songService ?? throw new ArgumentNullException(nameof(songService));
            _songsService = songsService ?? throw new ArgumentNullException(nameof(songsService));
        }

        /// <summary>
        /// Example: Create a new song and add it to a songs collection
        /// </summary>
        public async Task<SongResponseDto> CreateAndAddSongToCollectionAsync(int songsId)
        {
            // Create a new song DTO
            var createSongDto = new CreateSongDto
            {
                SongsId = songsId
            };

            // Use the service to create the song - this internally uses MappingService
            var createdSong = await _songService.CreateSongAsync(createSongDto);

            return createdSong;
        }

        /// <summary>
        /// Example: Get song details with collection information
        /// </summary>
        public async Task<SongResponseDto> GetSongWithCollectionDetailsAsync(int songId)
        {
            // Get song with related Songs collection - this uses MappingService internally
            var song = await _songService.GetSongWithSongsAsync(songId);
            return song;
        }

        /// <summary>
        /// Example: Search songs and return search-optimized DTOs
        /// </summary>
        public async Task<IEnumerable<SongSearchDto>> SearchSongsAsync(string title)
        {
            // Use search-specific DTOs that contain only search-relevant fields
            var searchResults = await _songService.SearchSongsByTitleAsync(title);
            return searchResults;
        }

        /// <summary>
        /// Example: Get songs collection with summary information
        /// </summary>
        public async Task<SongsSummaryDto> GetSongsCollectionSummaryAsync(int songsId)
        {
            // Get summary DTO with computed fields like TotalSongs and TotalDurationMinutes
            var summary = await _songsService.GetSongsSummaryAsync(songsId);
            return summary;
        }

        /// <summary>
        /// Example: Update song information
        /// </summary>
        public async Task<SongResponseDto> UpdateSongAsync(int songId, int newSongsId)
        {
            var updateDto = new UpdateSongDto
            {
                Id = songId,
                SongsId = newSongsId
            };

            // Update using DTOs - MappingService handles entity mapping internally
            var updatedSong = await _songService.UpdateSongAsync(songId, updateDto);
            return updatedSong;
        }

        /// <summary>
        /// Example: Get all songs in a list-optimized format
        /// </summary>
        public async Task<IEnumerable<SongListDto>> GetAllSongsForListDisplayAsync()
        {
            // Get list DTOs that contain only fields needed for list display
            var songs = await _songService.GetAllSongsAsync();
            return songs;
        }

        /// <summary>
        /// Example: Create a songs collection with multiple songs
        /// </summary>
        public async Task<SongsResponseDto> CreateSongsCollectionWithMultipleSongsAsync()
        {
            var createSongsDto = new CreateSongsDto
            {
                SongCollection = new List<CreateSongDto>
                {
                    new CreateSongDto { SongsId = 1 },
                    new CreateSongDto { SongsId = 1 },
                    new CreateSongDto { SongsId = 1 }
                }
            };

            // Create songs collection - MappingService maps DTOs to entities
            var createdSongsCollection = await _songsService.CreateSongsAsync(createSongsDto);
            return createdSongsCollection;
        }

        /// <summary>
        /// Example: Get recent songs with count limit
        /// </summary>
        public async Task<IEnumerable<SongListDto>> GetRecentSongsAsync(int count = 5)
        {
            var recentSongs = await _songService.GetRecentSongsAsync(count);
            return recentSongs;
        }

        /// <summary>
        /// Example: Check if song exists before performing operations
        /// </summary>
        public async Task<bool> SafeDeleteSongAsync(int songId)
        {
            // Check existence first
            if (!await _songService.SongExistsAsync(songId))
            {
                return false; // Song doesn't exist
            }

            // Perform deletion
            return await _songService.DeleteSongAsync(songId);
        }

        /// <summary>
        /// Example: Get songs collections with minimum song count
        /// </summary>
        public async Task<IEnumerable<SongsResponseDto>> GetPopulatedSongsCollectionsAsync(int minSongCount = 3)
        {
            var populatedCollections = await _songsService.GetSongsWithSongCountAsync(minSongCount);
            return populatedCollections;
        }
    }
}
