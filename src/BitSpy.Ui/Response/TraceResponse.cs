namespace BitSpy.Ui.Response;

public sealed class TraceResponse
{
    public required string Name { get; set; }
    public required long Duration { get; init; }
    public List<AttributeResponse> Attributes { get; init; } = new();
    public List<TraceEventRelationshipResponse> Events { get; init; } = new();
}