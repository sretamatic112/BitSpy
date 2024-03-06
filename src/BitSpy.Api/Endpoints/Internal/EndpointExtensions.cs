using System.Reflection;

namespace BitSpy.Api.Endpoints.Internal;

public static class EndpointExtensions
{
    public static void UseEndpoints(this IApplicationBuilder app)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining();

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoint.DefineEndpoints))!
                .Invoke(app, new object[] { app });
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining()
    {
        var endpointTypes = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(x => x is { IsAbstract: false, IsInterface: false } &&
                        typeof(IEndpoint).IsAssignableFrom(x));
        return endpointTypes;
    }
}