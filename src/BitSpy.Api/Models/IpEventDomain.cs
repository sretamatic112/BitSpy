namespace BitSpy.Api.Models;

public sealed class IpEventDomain
{
    public long RequestCounter { get; set; } = 0;
    public List<string> RequestIds { get; set; } = new();
}