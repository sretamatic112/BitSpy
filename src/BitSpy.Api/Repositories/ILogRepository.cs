using BitSpy.Api.Models;

namespace BitSpy.Api.Repositories;

public interface ILogRepository
{
    Task<bool> SaveAsync(LogDomain log);
    Task<IEnumerable<LogDomain>> GetLogsAsync(DateTime startingTimestamp, DateTime endingTimestamp);
    Task<LogDomain?> GetLogAsync(string level, DateTime timestamp);
    Task<bool> UpdateAsync(LogDomain log);
    Task<bool> DeleteAsync(string level, DateTime timestamp);
}