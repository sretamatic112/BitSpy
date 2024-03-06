namespace BitSpy.Api.Contracts.Database.Relationships;

public class TraceEventRelationship
{
    public long EventCounter { get; set; } = 0;
    public long EventAvgDuration { get; set; } = 0;
}