namespace BitSpy.Api.Contracts.Request;

public sealed record GetMetricRequest(
    string Name,
    decimal CpuUsage,
    DateTime TimeStamp);