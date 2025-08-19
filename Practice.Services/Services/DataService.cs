using LiteDB;
using Practice.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Services.Services
{
    public class DataService : IDataService
    {
        private readonly string dbName = "practice.db";

        private string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new ArgumentNullException("Assembly path is null");

        internal LiteDatabase GetDatabase()
        {

            using var database = new LiteDatabase(dbName);
            return database;
        }

        public Drill GetDrill(int id)
        {
            throw new NotImplementedException();
        }

        public List<Drill> GetDrills()
        {
            throw new NotImplementedException();
        }

        public Song GetSession(int id)
        {
            throw new NotImplementedException();
        }

        public List<Session> GetSessions()
        {
            using var db = new LiteDatabase(dbName);

        }

        public Song GetSong(string name)
        {
            throw new NotImplementedException();
        }

        public List<Song> GetSongs()
        {
            throw new NotImplementedException();
        }
    }
}
