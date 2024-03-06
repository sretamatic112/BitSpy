using BitSpy.Api.Models;
using Cassandra;
using ISession = Cassandra.ISession;

namespace BitSpy.Api.Repositories;

public class LogRepository : ILogRepository
{
    private readonly ISession _session;

    public LogRepository(IConfiguration configuration)
    {
        var cluster = Cluster.Builder()
            .AddContactPoint(configuration["Cassandra:Host"]!)
            .WithPort(int.Parse(configuration["Cassandra:Port"]!))
            .Build();

        _session = cluster.Connect(configuration["Cassandra:Keyspace"]);
    }

    public async Task<bool> SaveAsync(LogDomain log)
    {
        var query = await _session.PrepareAsync(
            "INSERT INTO logs (level, logTemplate, logValues, timestamp) VALUES (?, ?, ?, ?)");
        var bound = query.Bind(log.Level, log.LogTemplate, log.LogValues, log.Timestamp);
        var result = await _session.ExecuteAsync(bound);
        return result.IsFullyFetched;
    }

    public async Task<IEnumerable<LogDomain>> GetLogsAsync(DateTime startingTimestamp, DateTime endingTimestamp)
    {
        var query = await _session.PrepareAsync(
            "SELECT * FROM logs WHERE timestamp >= ? AND timestamp <= ? ALLOW FILTERING");
        var bound = query.Bind(startingTimestamp, endingTimestamp);
        var result = await _session.ExecuteAsync(bound);
        return result.Select(row => new LogDomain
        {
            Level = row.GetValue<string>("level"),
            LogTemplate = row.GetValue<string>("logTemplate".ToLower()),
            LogValues = row.GetValue<List<string>>("logValues".ToLower()),
            Timestamp = row.GetValue<DateTime>("timestamp")
        });
    }

    public async Task<LogDomain?> GetLogAsync(string level, DateTime timestamp)
    {
        var query = await _session.PrepareAsync("SELECT * FROM logs WHERE level = ? AND timestamp = ?");
        var bound = query.Bind(level, timestamp);
        var result = await _session.ExecuteAsync(bound);
        var row = result.FirstOrDefault();
        if (row is null)
        {
            return null;
        }

        return new LogDomain
        {
            Level = row.GetValue<string>("level"),
            LogTemplate = row.GetValue<string>("logTemplate".ToLower()),
            LogValues = row.GetValue<List<string>>("logValues".ToLower()),
            Timestamp = row.GetValue<DateTime>("timestamp")
        };
    }

    public async Task<bool> UpdateAsync(LogDomain log)
    {
        var query = await _session.PrepareAsync(
            "UPDATE bitspy.logs SET logValues = ?, logTemplate = ? WHERE level = ? AND timestamp = ?");
        var bound = query.Bind(log.LogValues, log.LogTemplate, log.Level, log.Timestamp);
        var result = await _session.ExecuteAsync(bound);
        return result.IsFullyFetched;
    }

    public async Task<bool> DeleteAsync(string level, DateTime timestamp)
    {
        var query = await _session.PrepareAsync("DELETE FROM logs WHERE level = ? AND timestamp = ?");
        var bound = query.Bind(level, timestamp);
        var result = await _session.ExecuteAsync(bound);
        return result.IsFullyFetched;
    }
}