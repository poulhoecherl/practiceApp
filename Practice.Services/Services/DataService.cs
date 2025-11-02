using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;
using System.Diagnostics;

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

        public async Task<Session> GetSession(int id)
        {
            try
            {
                using var context = new PracticeDbContext();
                
                var session = await context.Sessions.Where(m => m.Id == id).SingleOrDefaultAsync();
                if (session != null)
                {
                    return session;
                }

                return new Session();
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<Session>> GetSessions()
        {
            using (var context = new PracticeDbContext())
            {
                var sessions = await context.Sessions.ToListAsync();
                var retVal = sessions.Select(s => new Session()
                {
                    Id = s.Id,
                    
                }).ToList();

                return retVal;
            }
        }

        public async Task<List<Session>> GetOpenSessions()
        {
            using (var context = new PracticeDbContext())
            {
                var sessions = await context.Sessions.Where(m => m.StartTime == new DateTime(1901,1,1)).ToListAsync();
                if(sessions.Count != 0 == false)
                {
                    return [];
                }

                var retVal = sessions.Select(s => new Session()
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

        public async Task<Session> FinishSession(int SessionId)
        {
            try
            {
                using var context = new PracticeDbContext();

                // finishing the session
                var sesh = context.Sessions.Where(s => s.Id == SessionId);
                    //.ExecuteUpdateAsync(s => s.SetProperty(e => e.DurationMinutes, DateTime.Now));
                    
                await context.SaveChangesAsync();

                var session = await GetSession(SessionId);

                return new Session()
                {
                    Id = session.Id,
                    PracticeDate = session.PracticeDate,
                    Activity = session.Activity,
                    DurationMinutes = session.DurationMinutes,
                    EndTime = session.EndTime,
                    Notes = session.Notes,
                    StartTime = session.StartTime,
                    UserId = session.UserId,
                };

            }
            catch
            {

                throw;
            }

        }

        public void AddSessionsFromDto(List<SessionDto> logs)
        {
            try
            {
                using var context = new PracticeDbContext();

                foreach (var log in logs)
                {
                    Debug.WriteLine($" > Saving log entry: {log}");
                    // TODO: save to database using DataService
                    var session = new Session()
                    {
                        PracticeDate = log.PracticeDate,
                        StartTime = log.StartTime ?? log.PracticeDate,
                        EndTime = log.EndTime,
                        DurationMinutes = log.DurationMinutes,
                        Notes = log.Notes,
                        Activity = log.Activity,
                        RowCreatedBy = "import",
                        UserId = 1,
                    };

                    context.Sessions.Add(session);
                }

                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
