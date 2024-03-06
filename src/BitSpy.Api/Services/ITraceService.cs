using BitSpy.Api.Contracts.Response;
using BitSpy.Api.Models;

namespace BitSpy.Api.Services;

public interface ITraceService
{
    Task<bool> SaveAsync(TraceDomain trace);
    Task<bool> UpdateTraceAsync(string oldName, string newName);
    Task<bool> UpdateEventAsync(string name, TraceEventRelationshipDomain eventDomain);
    Task<bool> DeleteTraceAsync(string name);
    Task<bool> DeleteEventAsync(string name);

    Task<List<TraceResponse>> GetBottleneckTraceAsync(long duration, int traceCounter);
    Task<List<EventResponse>> GetBottleneckEventAsync(long duration, string traceName);
    Task<List<TraceResponse>> GetTracesForIpAsync(string ip);
}