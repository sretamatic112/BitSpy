namespace BitSpy.Api.Contracts.Database.Relationships;

public class IpUserTraceRelationship
{
    public required long RequestCounter { get; set; } = 0;
    public required List<string> RequestIds { get; init; }
}