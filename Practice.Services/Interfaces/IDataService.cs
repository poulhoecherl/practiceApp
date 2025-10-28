using Practice.Data.Models;
using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface IDataService
    {
        Task<List<Song>> GetSongs();

        Task<Song> GetSong(string name);

        Task<List<SessionResponseDto>> GetSessions();

        Task<SessionResponseDto> GetSession(int id);

        Task AddSession(Session newSession);

        Task DeleteSession(Session delSession);

        Task<UpdateSessionDto> FinishSession(int SessionId);

        Task<List<Drill>> GetDrills();

        Task<Drill> GetDrill(int id);
    }
}
