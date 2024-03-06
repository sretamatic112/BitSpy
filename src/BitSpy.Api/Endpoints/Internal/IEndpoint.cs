namespace BitSpy.Api.Endpoints.Internal;

public interface IEndpoint
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
}