using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Practice.Data;
using Practice.Data.Models;
using Practice.Services.DTOs;
using Practice.Services.Profiles;
using System.Collections.Generic;

namespace Practice.Services.Services
{
    public class DataService : IDataService
    {
        private string connectionString = string.Empty;

        private string dbName = "practice.db";

        // Add a private readonly IMapper field to the DataService class
        private readonly IMapper mapper;

        // Modify the constructor to accept IMapper
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
            throw new NotImplementedException();
        }

        public async Task<List<SessionResponseDto>> GetSessions()
        {
            using (var context = new PracticeDbContext())
            {
                var sessions = context.Sessions.ToList();
                var retVal = sessions.Select(s => new SessionResponseDto()
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
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

        public async Task<UpdateSessionDto> FinishLastSession()
        {
            try
            {
                using var context = new PracticeDbContext();

                var sessions = await GetSessions();

                var emptySession = sessions.Where(m => m.EndDate == new DateTime(1901, 1, 1)).OrderByDescending(m => m.Id).Take(1).SingleOrDefault();
                if (emptySession != null)
                {
                    // finishing the session
                    emptySession.EndDate = DateTime.Now;
                    await context.SaveChangesAsync();

                    return new UpdateSessionDto()
                    {
                        Id = emptySession.Id,
                        EndDate = emptySession.EndDate,
                        StartDate = emptySession.StartDate
                    };
                }
                return new UpdateSessionDto();
            }
            catch
            {

                throw;
            }

        }
    }
}
