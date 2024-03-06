namespace BitSpy.Api.Models;

public sealed class EventDomain
{
    public required string Name { get; init; }
    public required string Message { get; init; }
    public required long Duration { get; init; }
    public required List<AttributeDomain> Attributes { get; init; } = new();
}