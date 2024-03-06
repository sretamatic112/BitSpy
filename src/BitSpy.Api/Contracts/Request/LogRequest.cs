namespace BitSpy.Api.Contracts.Request;

public sealed class LogRequest
{
    public required string Level { get; init; }
    public required string LogTemplate { get; init; }
    public required List<string> LogValues { get; init; }
    public required DateTime Timestamp { get; init; }
}