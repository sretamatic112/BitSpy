using BitSpy.Api.Contracts.Database;
using BitSpy.Api.Contracts.Response;
using BitSpy.Api.Mappers;
using BitSpy.Api.Models;
using BitSpy.Api.Repositories;

namespace BitSpy.Api.Services;

public sealed class TraceService : ITraceService
{
    private readonly ILongTermTraceRepository _longTermTraceRepository;
    private readonly ITraceRepository _traceRepository;

    public TraceService(ITraceRepository traceRepository,
        ILongTermTraceRepository longTermTraceRepository)
    {
        _traceRepository = traceRepository;
        _longTermTraceRepository = longTermTraceRepository;
    }

    public async Task<bool> SaveAsync(TraceDomain trace)
    {
        var (existingIp, existingRelationship, existingTrace) =
            await _traceRepository.GetIpAndTraceAsync(trace.IpAddress, trace.Name);

        var transaction = await _traceRepository.BeginTransactionAsync();

        if (existingIp is null)
        {
            existingIp = await _traceRepository.CreateIpAsync(new IpUserContract
            {
                IpAddress = trace.IpAddress
            });
        }

        if (existingTrace is null)
        {
            existingTrace = await _traceRepository.CreateTraceAsync(new TraceContract
            {
                Name = trace.Name,
                Attributes = trace.Attributes,
                AverageDuration = trace.Duration,
                TraceCounter = 1
            });
        }
        else
        {
            existingTrace.AverageDuration =
                (existingTrace.AverageDuration * existingTrace.TraceCounter +
                 trace.Duration) / ++existingTrace.TraceCounter;

            await _traceRepository.UpdateAsync(existingTrace);
        }

        if (existingRelationship is null)
        {
            var longTermTraceId = await _longTermTraceRepository.SaveAsync(trace);
            await _traceRepository
                .CreateIpTraceRelationshipAsync(existingIp, existingTrace, longTermTraceId);
        }
        else
        {
            existingRelationship.RequestCounter++;
            existingRelationship.RequestIds.Add(await _longTermTraceRepository.SaveAsync(trace));
            await _traceRepository.UpdateAsync(existingIp.IpAddress, existingTrace.Name, existingRelationship);
        }

        var existingEvents = (await _traceRepository
                .GetEventsAsync(trace.Events.Select(x => x.Event.Name), existingTrace.Name))
            .ToList();

        var eventsWithRelationships = existingEvents
            .Where(x => x.Item2 is not null);

        foreach (var eventWithRelationship in eventsWithRelationships)
        {
            eventWithRelationship.Item2!.EventAvgDuration =
                (eventWithRelationship.Item2!.EventAvgDuration * eventWithRelationship.Item2!.EventCounter +
                 eventWithRelationship.Item1.Duration) / ++eventWithRelationship.Item2!.EventCounter;

            await _traceRepository.UpdateRelationshipAsync(existingTrace.Name, eventWithRelationship.Item1.Name,
                eventWithRelationship.Item2!);
        }

        var eventsWithoutRelationships = existingEvents
            .Where(x => x.Item2 is null)
            .Select(x => x.Item1);

        foreach (var eventWithoutRelationship in eventsWithoutRelationships)
        {
            await _traceRepository.AddRelationshipAsync(eventWithoutRelationship, existingTrace);
        }

        var eventsToAdd = trace.Events
            .Where(x => existingEvents.All(y => y.Item1.Name != x.Event.Name));

        foreach (var eventToAdd in eventsToAdd)
        {
            await _traceRepository.AddEventWithRelationshipAsync(existingTrace, eventToAdd.Event);
        }

        await transaction!.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateTraceAsync(string oldName, string newName)
    {
        var existingTrace = await _traceRepository.GetTraceByNameAsync(oldName);

        if (existingTrace is null)
        {
            return false;
        }
        
        existingTrace.Name = newName;

        await _traceRepository.UpdateTraceAsync(oldName, existingTrace);
        return true;
    }

    public async Task<bool> UpdateEventAsync(string name, TraceEventRelationshipDomain eventDomain)
    {
        var existingEvent = await _traceRepository.GetEventByNameAsync(name);

        if (existingEvent is null)
        {
            return false;
        }

        existingEvent.Name = eventDomain.Event.Name;
        existingEvent.Message = eventDomain.Event.Message;
        existingEvent.Attributes = eventDomain.Event.Attributes;

        await _traceRepository.UpdateEventAsync(name,existingEvent);

        return true;
    }

    public async Task<bool> DeleteTraceAsync(string name)
    {
        var existingTrace = await _traceRepository.GetTraceByNameAsync(name);

        if (existingTrace is null)
        {
            return false;
        }

        await _traceRepository.DeleteTraceAsync(name);

        return true;
    }

    public async Task<bool> DeleteEventAsync(string name)
    {
        var existingEvent = await _traceRepository.GetEventByNameAsync(name);

        if (existingEvent is null)
        {
            return false;
        }

        await _traceRepository.DeleteEventAsync(name);

        return true;
    }

    public async Task<List<TraceResponse>> GetBottleneckTraceAsync(long duration, int traceCounter)
    {
        var result = await _traceRepository.GetBottleneckTraceAsync(duration, traceCounter);

        return result.Select(x => x.ToContract()).ToList();
    }

    public async Task<List<EventResponse>> GetBottleneckEventAsync(long duration, string traceName)
    {
        var result = await _traceRepository.GetBottleneckEventAsync(duration, traceName);
        return result.Select(x => x.ToContract()).ToList();
    }

    public async Task<List<TraceResponse>> GetTracesForIpAsync(string ip)
    {
        var result = await _traceRepository.GetTracesForIpAsync(ip);
        return result.Select(x => x.ToContract()).ToList();
    }
}