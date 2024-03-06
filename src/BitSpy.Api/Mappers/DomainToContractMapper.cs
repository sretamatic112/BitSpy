using BitSpy.Api.Contracts.Response;
using BitSpy.Api.Models;

namespace BitSpy.Api.Mappers;

public static class DomainToContractMapper
{
    public static LogResponse ToContract(this LogDomain domain)
    {
        return new LogResponse()
        {
            Level = domain.Level,
            LogTemplate = domain.LogTemplate,
            LogValues = domain.LogValues,
            Timestamp = domain.Timestamp
        };
    }

    public static MetricResponse ToContract(this MetricDomain domain)
    {
        return new MetricResponse()
        {
            Name = domain.Name,
            TimeInGCSinceLastGCPercentage = domain.TimeInGCSinceLastGCPercentage,
            AllocationRatePerSecond = domain.AllocationRatePerSecond,
            CPUUsage = domain.CPUUsage,
            ExceptionCount = domain.ExceptionCount,
            Gen0CollectionCount = domain.Gen0CollectionCount,
            Gen0Size = domain.Gen0Size,
            Gen1CollectionCount = domain.Gen1CollectionCount,
            Gen1Size = domain.Gen1Size,
            Gen2CollectionCount = domain.Gen2CollectionCount,
            Gen2Size = domain.Gen2Size,
            ThreadPoolCompletedItemsCount = domain.ThreadPoolCompletedItemsCount,
            ThreadPoolQueueLength = domain.ThreadPoolQueueLength,
            ThreadPoolThreadCount = domain.ThreadPoolThreadCount,
            WorkingSet = domain.WorkingSet,
            Timestamp = domain.Timestamp
        };
    }

    public static TraceResponse ToContract(this TraceDomain domain)
    {
        return new TraceResponse
        {
            Name = domain.Name,
            Duration = domain.Duration,
            Attributes = domain.Attributes.Select(x => x.ToContract()).ToList(),
            Events = domain.Events.Select(x => x.ToContract()).ToList()
        };
    }

    public static AttributeResponse ToContract(this AttributeDomain domain)
    {
        return new AttributeResponse
        {
            Name = domain.Name,
            Value = domain.Value
        };
    }

    public static TraceEventRelationshipResponse ToContract(this TraceEventRelationshipDomain domain)
    {
        return new TraceEventRelationshipResponse
        {
            EventCounter = domain.EventCounter,
            EventAvgDuration = domain.EventAvgDuration,
            Event = new EventResponse
            {
                Name = domain.Event.Name,
                Message = domain.Event.Message,
                Attributes = domain.Event.Attributes.Select(x => x.ToContract())
                    .ToList(),
                Duration = domain.Event.Duration
            }
        };
    }

    public static EventResponse ToContract(this EventDomain domain)
    {
        return new EventResponse
        {
            Name = domain.Name,
            Message = domain.Message,
            Attributes = domain.Attributes.Select(x => x.ToContract()).ToList(),
            Duration = domain.Duration
        };
    }
}