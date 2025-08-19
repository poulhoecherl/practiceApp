using Microsoft.AspNetCore.Mvc;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Examples
{
    /// <summary>
    /// Example controller demonstrating how to consume SongsService with proper DTOs
    /// This would typically be in a separate Web/API project
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ISongsService _songsService;

        public SongsController(ISongsService songsService)
        {
            _songsService = songsService ?? throw new ArgumentNullException(nameof(songsService));
        }

        /// <summary>
        /// Get a songs collection by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SongsResponseDto>> GetSongs(int id)
        {
            try
            {
                var songs = await _songsService.GetSongsAsync(id);
                return Ok(songs);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Songs collection with id {id} not found");
            }
        }

        /// <summary>
        /// Get all songs collections
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongsListDto>>> GetAllSongs()
        {
            var songsCollections = await _songsService.GetAllSongsAsync();
            return Ok(songsCollections);
        }

        /// <summary>
        /// Get songs collection with full song details
        /// </summary>
        [HttpGet("{id}/details")]
        public async Task<ActionResult<SongsResponseDto>> GetSongsWithSongCollection(int id)
        {
            try
            {
                var songs = await _songsService.GetSongsWithSongCollectionAsync(id);
                return Ok(songs);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Songs collection with id {id} not found");
            }
        }

        /// <summary>
        /// Create a new songs collection
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SongsResponseDto>> CreateSongs([FromBody] CreateSongsDto createSongsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdSongs = await _songsService.CreateSongsAsync(createSongsDto);
                return CreatedAtAction(nameof(GetSongs), new { id = createdSongs.Id }, createdSongs);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing songs collection
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<SongsResponseDto>> UpdateSongs(int id, [FromBody] UpdateSongsDto updateSongsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedSongs = await _songsService.UpdateSongsAsync(id, updateSongsDto);
                return Ok(updatedSongs);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Songs collection with id {id} not found");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a songs collection
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSongs(int id)
        {
            var result = await _songsService.DeleteSongsAsync(id);
            if (!result)
            {
                return NotFound($"Songs collection with id {id} not found");
            }
            return NoContent();
        }

        /// <summary>
        /// Get songs collections with all songs loaded
        /// </summary>
        [HttpGet("withAllSongs")]
        public async Task<ActionResult<IEnumerable<SongsResponseDto>>> GetSongsWithAllSongs()
        {
            var songsCollections = await _songsService.GetSongsWithAllSongsAsync();
            return Ok(songsCollections);
        }

        /// <summary>
        /// Get songs collections with minimum song count
        /// </summary>
        [HttpGet("withMinimumCount/{minimumSongCount}")]
        public async Task<ActionResult<IEnumerable<SongsResponseDto>>> GetSongsWithSongCount(int minimumSongCount)
        {
            var songsCollections = await _songsService.GetSongsWithSongCountAsync(minimumSongCount);
            return Ok(songsCollections);
        }

        /// <summary>
        /// Get popular songs collections
        /// </summary>
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<SongsResponseDto>>> GetPopularSongsCollections()
        {
            var songsCollections = await _songsService.GetPopularSongsCollectionsAsync();
            return Ok(songsCollections);
        }

        /// <summary>
        /// Get songs collection summary
        /// </summary>
        [HttpGet("{id}/summary")]
        public async Task<ActionResult<SongsSummaryDto>> GetSongsSummary(int id)
        {
            try
            {
                var summary = await _songsService.GetSongsSummaryAsync(id);
                return Ok(summary);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Songs collection with id {id} not found");
            }
        }

        /// <summary>
        /// Check if songs collection exists
        /// </summary>
        [HttpHead("{id}")]
        public async Task<ActionResult> SongsExists(int id)
        {
            var exists = await _songsService.SongsExistsAsync(id);
            return exists ? Ok() : NotFound();
        }

        /// <summary>
        /// Check if songs collection has songs
        /// </summary>
        [HttpGet("{songsId}/hasSongs")]
        public async Task<ActionResult<bool>> HasSongs(int songsId)
        {
            var hasSongs = await _songsService.HasSongsAsync(songsId);
            return Ok(hasSongs);
        }

        /// <summary>
        /// Get song count for a specific songs collection
        /// </summary>
        [HttpGet("{songsId}/songCount")]
        public async Task<ActionResult<int>> GetSongCount(int songsId)
        {
            var count = await _songsService.GetSongCountAsync(songsId);
            return Ok(count);
        }

        /// <summary>
        /// Get total songs collections count
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetSongsCollectionCount()
        {
            var count = await _songsService.GetSongsCollectionCountAsync();
            return Ok(count);
        }
    }
}
