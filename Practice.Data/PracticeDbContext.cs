using Microsoft.EntityFrameworkCore;
using Practice.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Data
{
    public class PracticeDbContext : DbContext
    {
        public DbSet<Drill> Drills { get; set; }
        
        public DbSet<Song> Songs { get; set; }

        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set the database provider and connection string
            optionsBuilder.UseSqlite("Data Source=practice.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Session>().HasData(
            //    new Session { Id=1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(1) },
            //    new Session { Id = 2, StartDate = DateTime.Now.AddMinutes(2), EndDate = DateTime.Now.AddMinutes(3) }
            //);

            //modelBuilder.Entity<Drill>().HasData(
            //    new Drill { Id = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMinutes(1) },
            //    new Drill { Id = 2, StartDate = DateTime.Now.AddMinutes(2), EndDate = DateTime.Now.AddMinutes(3) }
            //);

            //modelBuilder.Entity<Song>().HasData(
            //    new Song { Id = 1, Name = "All of Me", Artist = "Oscar Peterson", Genre= "Jazz"},
            //    new Song { Id = 2, Name = "Recordame", Artist = "Joe Henderson", Genre = "Jazz" }
            //);
        }
    }
}
