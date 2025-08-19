using Practice.Data.Models;

namespace Practice.Data.Interfaces;

public interface IDrillRepository : IRepository<Drill>
{
    // Drill-specific methods
    Task<IEnumerable<Drill>> GetDrillsByDrillsIdAsync(int drillsId);
    Task<IEnumerable<Drill>> GetDrillsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Drill>> GetActiveDrillsAsync();
    Task<Drill?> GetLatestDrillAsync();
    Task<Drill?> GetDrillWithDrillsAsync(int id);
}
