namespace BitSpy.Api.Contracts.Request;

public sealed class GetAllLogsRequest
{
    public DateTime StartingTimeStamp { get; init; }
    public DateTime EndingTimeStamp { get; init; }
}