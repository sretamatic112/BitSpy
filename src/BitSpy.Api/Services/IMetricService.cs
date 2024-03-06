using BitSpy.Api.Models;

namespace BitSpy.Api.Services;

public interface IMetricService
{
    Task<bool> SaveAsync(MetricDomain metric);
    Task<IEnumerable<MetricDomain>> GetMetricsAsync(DateTime startingTimestamp, DateTime endingTimestamp);

    Task<MetricDomain?> GetMetricAsync(string name,
        int cpuUsage,
        DateTime timestamp);

    Task<bool> UpdateAsync(MetricDomain metric);

    Task<bool> DeleteAsync(string name,
        int cpuUsage,
        DateTime timestamp);
}