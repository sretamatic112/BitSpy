namespace BitSpy.Api.Contracts.Request;

public sealed record GetAllMetricsRequest(
    DateTime StartingTimeStamp,
    DateTime EndingTimeStamp);