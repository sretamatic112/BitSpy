namespace BitSpy.Api.Contracts.Response;

public sealed class LogResponse
{
    public required string Level { get; init; }
    public required string LogTemplate { get; init; }
    public required List<string> LogValues { get; init; }
    public required DateTime Timestamp { get; init; }
}