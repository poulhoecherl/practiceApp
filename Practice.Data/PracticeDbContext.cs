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
            var dbPath = @"D:\\Source\\practiceApp\\practice.db";
            // Set the database provider and connection string
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // No seed
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>();
            var currentUser = GetCurrentUser(); 
            var currentTime = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.RowCreatedOn = currentTime;
                        entry.Entity.RowCreatedBy = currentUser;
                        break;

                    case EntityState.Modified:
                        entry.Entity.RowModifiedOn = currentTime;
                        entry.Entity.RowModifiedBy = currentUser;
                        break;
                }
            }
        }

        // You'll need to implement this method based on your authentication system
        private string GetCurrentUser()
        {
            // Example implementations:
            return Environment.UserName; // For desktop apps
            // return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System"; // For web apps
            // return "System"; // For testing/demo purposes
            //return "System"; // Default implementation
        }
    }
}
