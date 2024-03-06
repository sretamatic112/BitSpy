namespace BitSpy.Ui.Response;

public sealed class EventResponse
{
    public required string Name { get; set; }
    public required string Message { get; init; }
    public required long Duration { get; init; }
    public string Label => $"{Name}: {Duration}ms";
    public required List<AttributeResponse> Attributes { get; init; } = new();
    
}