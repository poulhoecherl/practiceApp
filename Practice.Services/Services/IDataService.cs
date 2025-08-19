using Practice.Data.Models;

namespace Practice.Services.Services
{
    public interface IDataService
    {
        List<Song> GetSongs();

        Song GetSong(string name);

        List<Session> GetSessions();
        
        Song GetSession(int id);

        List<Drill> GetDrills();

        Drill GetDrill(int id);
    }
}
