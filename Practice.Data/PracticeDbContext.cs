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

        public DbSet<Session_Drill> SessionDrills { get; set; }
        public DbSet<Session_Song> SessionSongs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = @"D:\\Source\\practiceApp\\practice.db";
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure global query filters for soft deletion - NEVER return deleted records
            modelBuilder.Entity<Session>().HasQueryFilter(s => !s.Deleted);
            modelBuilder.Entity<Drill>().HasQueryFilter(d => !d.Deleted);
            modelBuilder.Entity<Song>().HasQueryFilter(s => !s.Deleted);
            modelBuilder.Entity<Session_Drill>().HasQueryFilter(sd => !sd.Deleted);
            modelBuilder.Entity<Session_Song>().HasQueryFilter(ss => !ss.Deleted);

            // Configure Session-Drill many-to-many relationship
            modelBuilder.Entity<Session_Drill>(entity =>
            {
                entity.HasKey(sd => sd.Id);

                entity.HasOne<Session>()
                    .WithMany()
                    .HasForeignKey(sd => sd.SessionId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to prevent hard deletes

                entity.HasOne<Drill>()
                    .WithMany()
                    .HasForeignKey(sd => sd.DrillId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to prevent hard deletes

                entity.HasIndex(sd => new { sd.SessionId, sd.DrillId }).IsUnique();
            });

            // Configure Session-Song many-to-many relationship
            modelBuilder.Entity<Session_Song>(entity =>
            {
                entity.HasKey(ss => ss.Id);

                entity.HasOne<Session>()
                    .WithMany()
                    .HasForeignKey(ss => ss.SessionId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to prevent hard deletes

                entity.HasOne<Song>()
                    .WithMany()
                    .HasForeignKey(ss => ss.SongId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to prevent hard deletes

                entity.HasIndex(ss => new { ss.SessionId, ss.SongId }).IsUnique();
            });

            // Configure many-to-many relationships through join entities
            modelBuilder.Entity<Session>()
                .HasMany<Drill>()
                .WithMany()
                .UsingEntity<Session_Drill>();

            modelBuilder.Entity<Session>()
                .HasMany<Song>()
                .WithMany()
                .UsingEntity<Session_Song>();
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
                        entry.Entity.Deleted = false; // Ensure new entities are not deleted
                        break;

                    case EntityState.Modified:
                        entry.Entity.RowModifiedOn = currentTime;
                        entry.Entity.RowModifiedBy = currentUser;
                        break;

                    case EntityState.Deleted:
                        // ALWAYS convert hard deletes to soft deletes
                        entry.State = EntityState.Modified;
                        entry.Entity.Deleted = true;
                        entry.Entity.RowModifiedOn = currentTime;
                        entry.Entity.RowModifiedBy = currentUser;
                        break;
                }
            }
        }

        private string GetCurrentUser()
        {
            return Environment.UserName;
        }

        // Method to get entities including deleted ones (for admin/audit purposes only)
        public IQueryable<T> GetAllIncludingDeleted<T>() where T : class, IAuditableEntity
        {
            return Set<T>().IgnoreQueryFilters();
        }

        // Method to get only deleted entities (for audit purposes)
        public IQueryable<T> GetDeleted<T>() where T : class, IAuditableEntity
        {
            return Set<T>().IgnoreQueryFilters().Where(e => e.Deleted);
        }

        // Method to restore soft-deleted entities (admin function)
        public async Task<bool> RestoreDeletedEntityAsync<T>(int id) where T : class, IAuditableEntity
        {
            var entity = await Set<T>().IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null || !entity.Deleted)
                return false;

            entity.Deleted = false;
            await SaveChangesAsync();
            return true;
        }
    }
}