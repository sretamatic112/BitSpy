namespace BitSpy.Api.Contracts.Request;

public sealed class EventRequest
{
    public required string Name { get; init; }
    public required string Message { get; init; }
    public DateTime Timestamp { get; init; }
    public required List<AttributeRequest> Attributes { get; init; } = new();
}