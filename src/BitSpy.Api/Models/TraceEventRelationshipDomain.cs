namespace BitSpy.Api.Models;

public sealed class TraceEventRelationshipDomain
{
    public required long EventCounter { get; set; } = 0;
    public required long EventAvgDuration { get; set; } = 0;
    public required EventDomain Event { get; set; }
}