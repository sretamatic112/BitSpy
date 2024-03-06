namespace BitSpy.Ui.Response;

public class TraceEventRelationshipResponse
{
    public long EventCounter { get; set; } = 0;
    public long EventAvgDuration { get; set; } = 0;
    public required EventResponse Event { get; init; }
}