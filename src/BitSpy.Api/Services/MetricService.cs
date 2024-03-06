using BitSpy.Api.Models;
using BitSpy.Api.Repositories;

namespace BitSpy.Api.Services;

public sealed class MetricService : IMetricService
{
    private readonly IMetricRepository _metricRepository;

    public MetricService(IMetricRepository metricRepository)
    {
        _metricRepository = metricRepository;
    }

    public async Task<bool> SaveAsync(MetricDomain metric)
    {
        return await _metricRepository.SaveAsync(metric);
    }

    public async Task<IEnumerable<MetricDomain>> GetMetricsAsync(DateTime startingTimestamp, DateTime endingTimestamp)
    {
        return await _metricRepository.GetMetricsAsync(startingTimestamp, endingTimestamp);
    }

    public async Task<MetricDomain?> GetMetricAsync(string name,
        int cpuUsage,
        DateTime timestamp)
    {
        return await _metricRepository.GetMetricAsync(name, cpuUsage, timestamp);
    }

    public async Task<bool> UpdateAsync(MetricDomain metric)
    {
        var existingMetric = await _metricRepository.GetMetricAsync(metric.Name, metric.CPUUsage, metric.Timestamp);
        if (existingMetric is null)
        {
            return false;
        }

        return await _metricRepository.UpdateAsync(metric);
    }

    public async Task<bool> DeleteAsync(string name,
        int cpuUsage,
        DateTime timestamp)
    {
        var existingMetric = await _metricRepository.GetMetricAsync(name, cpuUsage, timestamp);
        if (existingMetric is null)
        {
            return false;
        }

        return await _metricRepository.DeleteAsync(name, cpuUsage, timestamp);
    }
}