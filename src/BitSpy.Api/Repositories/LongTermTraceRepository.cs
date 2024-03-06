using System.Text.Json;
using BitSpy.Api.Models;
using Cassandra;
using ISession = Cassandra.ISession;

namespace BitSpy.Api.Repositories;

public sealed class LongTermTraceRepository : ILongTermTraceRepository
{
    private readonly ISession _session;

    public LongTermTraceRepository(IConfiguration configuration)
    {
        var cluster = Cluster.Builder()
            .AddContactPoint(configuration["Cassandra:Host"]!)
            .WithPort(int.Parse(configuration["Cassandra:Port"]!))
            .Build();

        _session = cluster.Connect(configuration["Cassandra:Keyspace"]);
    }
    
    public async Task<string> SaveAsync(TraceDomain trace)
    {
        var traceId = Guid.NewGuid().ToString();
        var query = await _session.PrepareAsync(
            "INSERT INTO traces (id, trace) VALUES (?, ?)");
        var bound = query.Bind(traceId, JsonSerializer.Serialize(trace));
        await _session.ExecuteAsync(bound);
        return traceId;
    }
}