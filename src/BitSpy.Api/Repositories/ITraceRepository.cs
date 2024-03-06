using BitSpy.Api.Contracts.Database;
using BitSpy.Api.Contracts.Database.Relationships;
using BitSpy.Api.Models;
using Neo4j.Driver;

namespace BitSpy.Api.Repositories;

public interface ITraceRepository
{
    Task<(IpUserContract?, IpUserTraceRelationship?, TraceContract?)> GetIpAndTraceAsync(string ipAddress,
        string traceName);

    Task<TraceContract> CreateTraceAsync(TraceContract trace);
    Task<IAsyncTransaction?> BeginTransactionAsync();
    Task<IpUserContract> CreateIpAsync(IpUserContract ip);
    Task UpdateAsync(TraceContract trace);

    Task UpdateAsync(string ip,
        string traceName,
        IpUserTraceRelationship ipUserTraceRelationship);

    Task CreateIpTraceRelationshipAsync(IpUserContract ip,
        TraceContract trace,
        string requestId);

    Task UpdateRelationshipAsync(string traceName,
        string eventName,
        TraceEventRelationship relationship);

    Task<IEnumerable<(EventContract, TraceEventRelationship?)>> GetEventsAsync(IEnumerable<string> names,
        string traceName);

    Task AddEventWithRelationshipAsync(TraceContract trace, EventDomain eventContract);
    Task AddRelationshipAsync(EventContract traceEventRelationship, TraceContract traceContract);
    Task<List<TraceDomain>> GetBottleneckTraceAsync(long duration, int traceCounter);
    Task<List<EventDomain>> GetBottleneckEventAsync(long duration, string? traceName);
    Task<List<TraceDomain>> GetTracesForIpAsync(string ip);
    Task<TraceContract?> GetTraceByNameAsync(string name);
    Task<EventContract?> GetEventByNameAsync(string name);
    Task UpdateEventAsync(string eventName,EventContract eventContract);
    Task UpdateTraceAsync(string oldName,TraceContract traceContract);
    Task DeleteTraceAsync(string name);
    Task DeleteEventAsync(string name);
}