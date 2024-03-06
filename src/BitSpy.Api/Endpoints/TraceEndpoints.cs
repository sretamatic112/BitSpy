using BitSpy.Api.Contracts.Request;
using BitSpy.Api.Endpoints.Internal;
using BitSpy.Api.Mappers;
using BitSpy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitSpy.Api.Endpoints;

public class TraceEndpoints : IEndpoint
{
    private const string BaseRoute = "/traces";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreateTrace);
        app.MapGet( "/bottleneck/traces", GetBottleneckTraces);
        app.MapGet("/bottleneck/events", GetBottleneckEvents);
        app.MapGet(BaseRoute + "/ip/{ip}", GetTracesForIp);
        app.MapPut("/traces/{oldName}", UpdateTrace);
        app.MapPut("/events/{eventName}", UpdateEvent);
        app.MapDelete("/traces/{traceName}", DeleteTrace);
        app.MapDelete("/events/{eventName}", DeleteEvent);
    }

    private static async Task<IResult> UpdateEvent(string eventName,
        [FromBody] EventRequest eventRequest,
        ITraceService traceService)
    {
        var result = await traceService.UpdateEventAsync(eventName, eventRequest.ToDomain());
        return result
            ? Results.Ok()
            : Results.BadRequest();
    }

    private static async Task<IResult> UpdateTrace(string oldName,
        [FromBody]string newName,
        ITraceService traceService)
    {
        var result = await traceService.UpdateTraceAsync(oldName, newName);
        return result
            ? Results.Ok()
            : Results.BadRequest();
    }

    private static async Task<IResult> DeleteEvent(string eventName,
        ITraceService traceService)
    {
        var result = await traceService.DeleteEventAsync(eventName);
        return result
            ? Results.Ok()
            : Results.BadRequest();
    }

    private static async Task<IResult> DeleteTrace(string traceName,
        ITraceService traceService)
    {
        var result = await traceService.DeleteTraceAsync(traceName);
        return result
            ? Results.Ok()
            : Results.BadRequest();
    }

    private static async Task<IResult> CreateTrace(
        TraceRequest traceRequest,
        ITraceService traceService)
    {
        var result = await traceService.SaveAsync(traceRequest.ToDomain());
        return result ? Results.Created() : Results.BadRequest();
    }

    private static async Task<IResult> GetBottleneckTraces(
        long? duration,
        int? traceCounter,
        ITraceService traceService)
    {
        var result = await traceService.GetBottleneckTraceAsync(duration ?? 0, traceCounter ?? 1);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetBottleneckEvents(
        long? duration,
        string? traceName,
        ITraceService traceService)
    {
        var result = await traceService.GetBottleneckEventAsync(duration ?? 0, traceName ?? "");
        return Results.Ok(result);
    }


    private static async Task<IResult> GetTracesForIp(
        string ip,
        ITraceService traceService)
    {
        var result = await traceService.GetTracesForIpAsync(ip);
        return Results.Ok(result);
    }
}