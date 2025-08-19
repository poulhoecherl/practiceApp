using Practice.Data.Models;

namespace Practice.Data.Interfaces;

public interface IDrillsRepository : IRepository<Drills>
{
    // Drills-specific methods
    Task<Drills?> GetDrillsWithDrillCollectionAsync(int id);
    Task<IEnumerable<Drills>> GetDrillsWithAllDrillsAsync();
    Task<IEnumerable<Drills>> GetDrillsWithDrillCountAsync(int minimumDrillCount);
    Task<bool> HasDrillsAsync(int drillsId);
    Task<int> GetDrillCountAsync(int drillsId);
}
