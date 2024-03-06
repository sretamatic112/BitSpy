namespace BitSpy.Api.Contracts.Response;

public sealed class AttributeResponse
{
    public required string Name { get; init; }
    public required string Value { get; init; }
}