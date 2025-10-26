using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Services.Services
{
    public class DataService : IDataService
    {
        private string connectionString = string.Empty;

        private string dbName = "practice.db";

        public DataService()
        {
            // Initialize the database path
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo currentDir = new DirectoryInfo(baseDirectory);
            DirectoryInfo twoLevelsUp = currentDir.Parent?.Parent?.Parent?.Parent;
            string dbFolder = twoLevelsUp?.FullName;

            dbName = Path.Combine(dbFolder, dbName);

            if (File.Exists(dbName))
            {
                connectionString = $"DataSource= {dbName}";
            }

        }

        public async Task<Drill> GetDrill(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Drill>> GetDrills()
        {
            throw new NotImplementedException();
        }

        public async Task<SessionResponseDto> GetSession(int id)
        {
            try
            {
                using var context = new PracticeDbContext();
                
                var session = await context.Sessions.Where(m => m.Id == id).SingleOrDefaultAsync();
                if (session != null)
                {

                    var retVal = new SessionResponseDto()
                    {
                        Id = session.Id,
                        
                    };

                    return retVal;
                }

                return new SessionResponseDto();
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<SessionResponseDto>> GetSessions()
        {
            using (var context = new PracticeDbContext())
            {
                var sessions = await context.Sessions.ToListAsync();
                var retVal = sessions.Select(s => new SessionResponseDto()
                {
                    Id = s.Id,
                    
                }).ToList();

                return retVal;
            }
        }

        public async Task<List<SessionResponseDto>> GetOpenSessions()
        {
            using (var context = new PracticeDbContext())
            {
                var sessions = await context.Sessions.Where(m => m.StartTime == new DateTime(1901,1,1)).ToListAsync();
                if(sessions.Count != 0 == false)
                {
                    return [];
                }

                var retVal = sessions.Select(s => new SessionResponseDto()
                {
                    Id = s.Id,
                    
                }).ToList();

                return retVal;
            }
        }

        public async Task AddSession(Session newSession)
        {
            try
            {
                using (var context = new PracticeDbContext())
                {
                    await context.Sessions.AddAsync(newSession);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {

                throw;
            }
        }

        public async Task DeleteSession(Session delSession)
        {
            try
            {
                using (var context = new PracticeDbContext())
                {
                    context.Sessions.Remove(delSession);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {

                throw;
            }
        }

        public async Task<Song> GetSong(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Song>> GetSongs()
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateSessionDto> FinishSession(int SessionId)
        {
            try
            {
                using var context = new PracticeDbContext();

                // finishing the session
                var sesh = context.Sessions.Where(s => s.Id == SessionId);
                    //.ExecuteUpdateAsync(s => s.SetProperty(e => e.DurationMinutes, DateTime.Now));
                    
                await context.SaveChangesAsync();

                var session = await GetSession(SessionId);

                return new UpdateSessionDto()
                {
                    Id = session.Id,
                    EndDate = session.EndDate,
                    StartDate = session.StartDate
                };
            }
            catch
            {

                throw;
            }

        }
    }
}
