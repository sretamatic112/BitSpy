namespace BitSpy.Api.Contracts.Request;

public sealed class TraceRequest
{
    public required string IpAddress { get; init; }
    public required string Name { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public List<AttributeRequest> Attributes { get; init; } = new();
    public List<EventRequest> Events { get; init; } = new();
}