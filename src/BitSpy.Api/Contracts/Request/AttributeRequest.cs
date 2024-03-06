﻿namespace BitSpy.Api.Contracts.Request;

public sealed class AttributeRequest
{
    public required string Name { get; init; }
    public required string Value { get; init; }
}