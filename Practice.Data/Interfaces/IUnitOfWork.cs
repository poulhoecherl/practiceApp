namespace Practice.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repository properties
    IDrillRepository Drills { get; }
    IDrillsRepository DrillsCollections { get; }
    ISessionRepository Sessions { get; }
    ISongRepository Songs { get; }
    ISongsRepository SongsCollections { get; }

    // Transaction management
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    // Transaction control
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    
    // Bulk operations
    Task<int> ExecuteRawSqlAsync(string sql, params object[] parameters);
    Task<int> ExecuteRawSqlAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
}
