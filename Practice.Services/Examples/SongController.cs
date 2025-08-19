using Microsoft.AspNetCore.Mvc;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Examples
{
    /// <summary>
    /// Example controller demonstrating how to consume SongService with proper DTOs
    /// This would typically be in a separate Web/API project
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService ?? throw new ArgumentNullException(nameof(songService));
        }

        /// <summary>
        /// Get a song by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SongResponseDto>> GetSong(int id)
        {
            try
            {
                var song = await _songService.GetSongAsync(id);
                return Ok(song);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Song with id {id} not found");
            }
        }

        /// <summary>
        /// Get all songs
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongListDto>>> GetAllSongs()
        {
            var songs = await _songService.GetAllSongsAsync();
            return Ok(songs);
        }

        /// <summary>
        /// Get songs by Songs collection ID
        /// </summary>
        [HttpGet("bySongsId/{songsId}")]
        public async Task<ActionResult<IEnumerable<SongListDto>>> GetSongsBySongsId(int songsId)
        {
            var songs = await _songService.GetSongsBySongsIdAsync(songsId);
            return Ok(songs);
        }

        /// <summary>
        /// Create a new song
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SongResponseDto>> CreateSong([FromBody] CreateSongDto createSongDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdSong = await _songService.CreateSongAsync(createSongDto);
                return CreatedAtAction(nameof(GetSong), new { id = createdSong.Id }, createdSong);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing song
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<SongResponseDto>> UpdateSong(int id, [FromBody] UpdateSongDto updateSongDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedSong = await _songService.UpdateSongAsync(id, updateSongDto);
                return Ok(updatedSong);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Song with id {id} not found");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a song
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSong(int id)
        {
            var result = await _songService.DeleteSongAsync(id);
            if (!result)
            {
                return NotFound($"Song with id {id} not found");
            }
            return NoContent();
        }

        /// <summary>
        /// Search songs by title
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SongSearchDto>>> SearchSongs([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title parameter is required");
            }

            var songs = await _songService.SearchSongsByTitleAsync(title);
            return Ok(songs);
        }

        /// <summary>
        /// Get favorite songs
        /// </summary>
        [HttpGet("favorites")]
        public async Task<ActionResult<IEnumerable<SongListDto>>> GetFavoriteSongs()
        {
            var songs = await _songService.GetFavoriteSongsAsync();
            return Ok(songs);
        }

        /// <summary>
        /// Get recent songs
        /// </summary>
        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<SongListDto>>> GetRecentSongs([FromQuery] int count = 10)
        {
            var songs = await _songService.GetRecentSongsAsync(count);
            return Ok(songs);
        }

        /// <summary>
        /// Get song summary
        /// </summary>
        [HttpGet("{id}/summary")]
        public async Task<ActionResult<SongSummaryDto>> GetSongSummary(int id)
        {
            try
            {
                var summary = await _songService.GetSongSummaryAsync(id);
                return Ok(summary);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Song with id {id} not found");
            }
        }

        /// <summary>
        /// Check if song exists
        /// </summary>
        [HttpHead("{id}")]
        public async Task<ActionResult> SongExists(int id)
        {
            var exists = await _songService.SongExistsAsync(id);
            return exists ? Ok() : NotFound();
        }

        /// <summary>
        /// Get total song count
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetSongCount()
        {
            var count = await _songService.GetSongCountAsync();
            return Ok(count);
        }
    }
}
