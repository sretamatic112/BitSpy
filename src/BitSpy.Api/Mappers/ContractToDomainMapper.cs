using BitSpy.Api.Contracts.Request;
using BitSpy.Api.Models;

namespace BitSpy.Api.Mappers;

public static class ContractToDomainMapper
{
    public static LogDomain ToDomain(this LogRequest request)
    {
        return new LogDomain
        {
            Level = request.Level,
            Timestamp = request.Timestamp,
            LogTemplate = request.LogTemplate,
            LogValues = request.LogValues
        };
    }

    public static MetricDomain ToDomain(this MetricRequest request)
    {
        return new MetricDomain
        {
            Name = request.Name,
            TimeInGCSinceLastGCPercentage = request.TimeInGCSinceLastGCPercentage,
            AllocationRatePerSecond = request.AllocationRatePerSecond,
            CPUUsage = request.CPUUsage,
            ExceptionCount = request.ExceptionCount,
            Gen0CollectionCount = request.Gen0CollectionCount,
            Gen0Size = request.Gen0Size,
            Gen1CollectionCount = request.Gen1CollectionCount,
            Gen1Size = request.Gen1Size,
            Gen2CollectionCount = request.Gen2CollectionCount,
            Gen2Size = request.Gen2Size,
            ThreadPoolCompletedItemsCount = request.ThreadPoolCompletedItemsCount,
            ThreadPoolQueueLength = request.ThreadPoolQueueLength,
            ThreadPoolThreadCount = request.ThreadPoolThreadCount,
            WorkingSet = request.WorkingSet,
            Timestamp = request.Timestamp
        };
    }

    public static TraceDomain ToDomain(this TraceRequest request)
    {
        return new TraceDomain
        {
            Name = request.Name,
            Duration = (long)(request.EndTime - request.StartTime).TotalMilliseconds,
            Attributes = request.Attributes.Select(x => x.ToDomain())
                .ToList(),
            Events = request.Events.Select(x => x.ToDomain())
                .ToList(),
            IpAddress = request.IpAddress
        };
    }

    public static AttributeDomain ToDomain(this AttributeRequest request)
    {
        return new AttributeDomain
        {
            Name = request.Name,
            Value = request.Value
        };
    }

    public static TraceEventRelationshipDomain ToDomain(this EventRequest request)
    {
        return new TraceEventRelationshipDomain
        {
            EventCounter = 0,
            EventAvgDuration = 0,
            Event = new EventDomain
            {
                Name = request.Name,
                Message = request.Message,
                Attributes = request.Attributes.Select(x => x.ToDomain())
                    .ToList(),
                Duration = request.Timestamp.Millisecond
            }
        };
    }
}