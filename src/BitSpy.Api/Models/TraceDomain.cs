namespace BitSpy.Api.Models;

public sealed class TraceDomain
{
    public required string Name { get; init; }
    public required long Duration { get; init; }
    public required string IpAddress { get; init; }
    public List<AttributeDomain> Attributes { get; init; } = new();
    public List<TraceEventRelationshipDomain> Events { get; init; } = new();
}