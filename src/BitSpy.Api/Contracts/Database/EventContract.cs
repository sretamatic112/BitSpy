using BitSpy.Api.Models;

namespace BitSpy.Api.Contracts.Database;

public sealed class EventContract
{
    public required string Name { get; set; }
    public required string Message { get; set; }
    public required long Duration { get; init; }
    public required List<AttributeDomain> Attributes { get; set; } = new();
}