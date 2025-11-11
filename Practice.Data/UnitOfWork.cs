using Microsoft.EntityFrameworkCore;
using Practice.Data.Interfaces;
using Practice.Data.Repositories;


namespace Practice.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory<PracticeDbContext> _contextFactory;
        private PracticeDbContext? _context;
        
        // Lazy-loaded repositories
        private Practice.Data.Interfaces.IDrillRepository? _drills;
        private IDrillsRepository? _drillsCollections;
        private ISessionRepository? _sessions;
        private ISongRepository? _songs;
        private ISongsRepository? _songsCollections;

        public UnitOfWork(IDbContextFactory<PracticeDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        private PracticeDbContext Context
        {
            get
            {
                _context ??= _contextFactory.CreateDbContext();
                return _context;
            }
        }

        public IDrillRepository Drills => _drills ??= new DrillRepository(Context);
        public IDrillsRepository DrillsCollections => _drillsCollections ??= new DrillsRepository(Context);
        public ISessionRepository Sessions => _sessions ??= new SessionRepository(Context);
        public ISongRepository Songs => _songs ??= new SongRepository(Context);
        public ISongsRepository SongsCollections => _songsCollections ??= new SongsRepository(Context);

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await Context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (Context.Database.CurrentTransaction != null)
            {
                await Context.Database.CurrentTransaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (Context.Database.CurrentTransaction != null)
            {
                await Context.Database.CurrentTransaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task<int> ExecuteRawSqlAsync(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteRawSqlAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}