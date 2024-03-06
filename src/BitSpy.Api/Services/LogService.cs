using BitSpy.Api.Models;
using BitSpy.Api.Repositories;

namespace BitSpy.Api.Services;

public sealed class LogService : ILogService
{
    private readonly ILogRepository _logRepository;

    public LogService(ILogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<bool> SaveAsync(LogDomain log)
    {
        return await _logRepository.SaveAsync(log);
    }

    public async Task<IEnumerable<LogDomain>> GetLogsAsync(DateTime startingTimestamp, DateTime endingTimestamp)
    {
        return await _logRepository.GetLogsAsync(startingTimestamp, endingTimestamp);
    }

    public async Task<LogDomain?> GetLogAsync(string level, DateTime timestamp)
    {
        return await _logRepository.GetLogAsync(level, timestamp);
    }

    public async Task<bool> UpdateAsync(LogDomain log)
    {
        var existingLog = await _logRepository.GetLogAsync(log.Level, log.Timestamp);
        if (existingLog is null)
        {
            return false;
        }

        return await _logRepository.UpdateAsync(log);
    }

    public async Task<bool> DeleteAsync(string level, DateTime timestamp)
    {
        var existingLog = await _logRepository.GetLogAsync(level, timestamp);
        if (existingLog is null)
        {
            return false;
        }

        return await _logRepository.DeleteAsync(level, timestamp);
    }
}