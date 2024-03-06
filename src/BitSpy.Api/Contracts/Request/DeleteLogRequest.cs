namespace BitSpy.Api.Contracts.Request;

public class DeleteLogRequest
{
    public required string Level { get; init; }
    public required DateTime Timestamp { get; init; }
}