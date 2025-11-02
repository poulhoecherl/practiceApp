using Practice.Data.Models;
using Practice.Services.DTOs;

namespace Practice.Services.Interfaces
{
    public interface IDataService
    {
        Task<List<Song>> GetSongs();

        Task<Song> GetSong(string name);

        Task<List<Session>> GetSessions();

        Task<Session> GetSession(int id);

        Task AddSession(Session newSession);

        Task DeleteSession(Session delSession);

        Task<Session> FinishSession(int SessionId);

        Task<List<Drill>> GetDrills();

        Task<Drill> GetDrill(int id);
    }
}
