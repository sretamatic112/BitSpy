using BitSpy.Api.Models;

namespace BitSpy.Api.Services;

public interface ILogService
{
    Task<bool> SaveAsync(LogDomain log);
    Task<IEnumerable<LogDomain>> GetLogsAsync(DateTime startingTimestamp, DateTime endingTimestamp);
    Task<LogDomain?> GetLogAsync(string level, DateTime timestamp);
    Task<bool> UpdateAsync(LogDomain log);
    Task<bool> DeleteAsync(string level, DateTime timestamp);
}