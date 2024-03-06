using Cassandra;
using ISession = Cassandra.ISession;

namespace BitSpy.Api;

public static class DatabaseInitializer
{
    private static ISession _session;

    public static async Task InitializeAsync(IConfiguration configuration)
    {
        var cluster = Cluster.Builder()
            .AddContactPoint(configuration["Cassandra:Host"]!)
            .WithPort(int.Parse(configuration["Cassandra:Port"]!))
            .Build();

        _session = await cluster.ConnectAsync();

        var keyspace = configuration["Cassandra:Keyspace"]!;

        if (!await KeyspaceExistsAsync(cluster, keyspace))
        {
            // Keyspace doesn't exist, create it
            await CreateKeyspaceAsync(cluster, keyspace);
        }

        _session = await cluster.ConnectAsync(keyspace);
        await CreateMetricsTableAsync();
        await CreateLogsTableAsync();
        await CreateTraceTableAsync();
    }
    
    private static async Task CreateTraceTableAsync()
    {
        var query = @"CREATE TABLE IF NOT EXISTS traces (
                    id uuid,
                    trace text,
                    PRIMARY KEY (id)
                  );";
        await _session.ExecuteAsync(new SimpleStatement(query));
    }

    private static async Task<bool> KeyspaceExistsAsync(ICluster cluster, string keyspace)
    {
        // Check if the keyspace exists
        var keyspaces = cluster.Metadata.GetKeyspaces().ToList();
        return keyspaces.Contains(keyspace.ToLower());
    }

    private static async Task CreateKeyspaceAsync(ICluster cluster, string keyspace)
    {
        // Create the keyspace
        var query = await _session.PrepareAsync(
            $"CREATE KEYSPACE {keyspace} WITH replication = {{'class': 'SimpleStrategy', 'replication_factor': 1}}");
        var bond = query.Bind();
        await _session.ExecuteAsync(bond);
    }

    private static async Task CreateMetricsTableAsync()
    {
        var query = @"CREATE TABLE IF NOT EXISTS metrics (
                        name text,
                        timeInGCSinceLastGCPercentage int,
                        allocationRatePerSecond int,
                        cpuUsage int,
                        exceptionCount int,
                        gen0CollectionCount int,
                        gen0Size int,
                        gen1CollectionCount int,
                        gen1Size int,
                        gen2CollectionCount int,
                        gen2Size int,
                        threadPoolCompletedItemsCount int,
                        threadPoolQueueLength int,
                        threadPoolThreadCount int,
                        workingSet int,
                        timestamp timestamp,
                        PRIMARY KEY (name, cpuUsage, timestamp)
                      );";
        await _session.ExecuteAsync(new SimpleStatement(query));
    }

    private static async Task CreateLogsTableAsync()
    {
        var query = @"CREATE TABLE IF NOT EXISTS logs (
                        level text,
                        logTemplate text,
                        logValues list<text>,
                        timestamp timestamp,
                        PRIMARY KEY (level, timestamp)
                      );";
        await _session.ExecuteAsync(new SimpleStatement(query));
    }
}