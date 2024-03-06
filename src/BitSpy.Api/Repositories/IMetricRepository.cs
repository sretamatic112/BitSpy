using BitSpy.Api.Models;

namespace BitSpy.Api.Repositories;

public interface IMetricRepository
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