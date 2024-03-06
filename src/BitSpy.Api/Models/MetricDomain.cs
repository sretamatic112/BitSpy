namespace BitSpy.Api.Models;

public class MetricDomain
{
    public required string Name { get; init; }
    public required int TimeInGCSinceLastGCPercentage { get; init; }
    public required int AllocationRatePerSecond { get; init; }
    public required int CPUUsage { get; init; }
    public required int ExceptionCount { get; init; }
    public required int Gen0CollectionCount { get; init; }
    public required int Gen0Size { get; init; }
    public required int Gen1CollectionCount { get; init; }
    public required int Gen1Size { get; init; }
    public required int Gen2CollectionCount { get; init; }
    public required int Gen2Size { get; init; }
    public required int ThreadPoolCompletedItemsCount { get; init; }
    public required int ThreadPoolQueueLength { get; init; }
    public required int ThreadPoolThreadCount { get; init; }
    public required int WorkingSet { get; init; }
    public required DateTime Timestamp { get; init; }
}