using BitSpy.Api.Contracts.Request;
using BitSpy.Api.Endpoints.Internal;
using BitSpy.Api.Mappers;
using BitSpy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitSpy.Api.Endpoints;

public class LogEndpoints : IEndpoint
{
    private const string BaseRoute = "/logs";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, CreateLog);
        app.MapDelete(BaseRoute, DeleteLog);
        app.MapPut(BaseRoute, UpdateLog);
        app.MapGet(BaseRoute, GetLogs);
        app.MapGet(BaseRoute + "/{name}", GetLog);
    }

    private static async Task<IResult> CreateLog(
        [FromBody] LogRequest logRequest,
        ILogService logService
    )
    {
        var success = await logService.SaveAsync(logRequest.ToDomain());
        return success ? Results.Created() : Results.BadRequest();
    }

    private static async Task<IResult> DeleteLog(
        [FromBody] DeleteLogRequest logRequest,
        ILogService logService)
    {
        var deleted = await logService.DeleteAsync(logRequest.Level, logRequest.Timestamp);

        return deleted ? Results.Ok() : Results.NotFound();
    }

    private static async Task<IResult> UpdateLog(
        [FromBody] LogRequest logRequest,
        ILogService logService)
    {
        var updated = await logService.UpdateAsync(logRequest.ToDomain());

        return updated ? Results.Ok() : Results.NotFound();
    }

    private static async Task<IResult> GetLogs(
        DateTime startingTimeStamp,
        DateTime endingTimeStamp,
        ILogService logService)
    {
        var logList =
            await logService.GetLogsAsync(startingTimeStamp, endingTimeStamp);
        return Results.Ok(logList);
    }

    private static async Task<IResult> GetLog(
        string level,
        DateTime startingTimeStamp,
        ILogService logService)
    {
        var result =
            await logService.GetLogAsync(level, startingTimeStamp);

        return result is null ? Results.NotFound() : Results.Ok(result);
    }
}