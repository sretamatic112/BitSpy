using BitSpy.Api.Contracts.Request;
using BitSpy.Api.Endpoints.Internal;
using BitSpy.Api.Mappers;
using BitSpy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitSpy.Api.Endpoints;

public class MetricEndpoints : IEndpoint
{
    private const string BaseRoute = "/metrics";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, AddMetric);
        app.MapGet(BaseRoute + "/{name}", GetMetric);
        app.MapGet(BaseRoute + "/all", GetAll);
        app.MapPut(BaseRoute, UpdateMetric);
        app.MapDelete(BaseRoute, DeleteMetric);
    }

    private static async Task<IResult> AddMetric(
        [FromBody] MetricRequest metricRequest,
        IMetricService metricService)
    {
        var added = await metricService.SaveAsync(metricRequest.ToDomain());

        return added ? Results.Created() : Results.Problem();
    }

    private static async Task<IResult> GetMetric(
        [FromRoute] string name,
        int cpuUsage,
        DateTime timeStamp,
        IMetricService metricService)
    {
        var metric = await metricService.GetMetricAsync(name, cpuUsage, timeStamp);

        return metric is null ? Results.NotFound() : Results.Ok(metric);
    }

    private static async Task<IResult> GetAll(
        DateTime startingTimeStamp,
        DateTime endingTimeStamp,
        IMetricService metricService)
    {
        var result =
            await metricService.GetMetricsAsync(startingTimeStamp, endingTimeStamp);
        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateMetric(
        [FromBody] MetricRequest metricRequest,
        IMetricService metricService)
    {
        var updated = await metricService.UpdateAsync(metricRequest.ToDomain());

        return updated ? Results.Ok() : Results.NotFound();
    }

    private static async Task<IResult> DeleteMetric(string name,
        int cpuUsage,
        DateTime timeStamp,
        IMetricService metricService)
    {
        var deleted = await metricService.DeleteAsync(name, cpuUsage, timeStamp);

        return deleted ? Results.Ok() : Results.NotFound();
    }
}