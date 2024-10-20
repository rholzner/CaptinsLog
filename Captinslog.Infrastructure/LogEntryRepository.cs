using Captinslog.Domain;
using FlowCode;

namespace Captinslog.Infrastructure;

public class LogEntryRepository : ILogEntryRepository
{
    private readonly LogEntryDatabaseContext _db;
    private readonly ILogEntryDatabaseContextHelper _dbHelper;
    private readonly IJsonSerializer _jsonSerializer;

    public LogEntryRepository(IJsonSerializer jsonSerializer, LogEntryDatabaseContext logEntryDatabaseContext, ILogEntryDatabaseContextHelper logEntryDatabaseContextHelper)
    {
        _jsonSerializer = jsonSerializer;
        _db = logEntryDatabaseContext;
        _dbHelper = logEntryDatabaseContextHelper;
    }


    public OperationResult Add(LogEntry logEntry)
    {
        var story = _dbHelper.GetOrCreateStory(logEntry.StoryId);
        return story.OnSuccess(s =>
        {
            var correlation = _dbHelper.GetOrCreateCorrelation(s, logEntry.CorrelationId);

            return correlation.OnSuccess(c =>
            {
                return _dbHelper.CreateLogEntry(c, logEntry, null);
            });
        });
    }

    public OperationResult Add<T>(LogEntry<T> logEntry)
    {
        var story = _dbHelper.GetOrCreateStory(logEntry.StoryId);
        return story.OnSuccess(s =>
        {
            var correlationData = _dbHelper.GetOrCreateCorrelation(s, logEntry.CorrelationId);

            return correlationData.OnSuccess(correlation =>
            {
                var jsonData = _jsonSerializer.Serialize(logEntry.Data);

                return jsonData.OnSuccess<string>(json =>
                {
                    return _dbHelper.CreateLogEntry(correlation, logEntry, json);
                });
            });
        });
    }

    public async ValueTask<OperationResult> AddAsync(LogEntry logEntry)
    {
        var story = _dbHelper.GetOrCreateStory(logEntry.StoryId);
        return await story.OnSuccessAsync(async s =>
        {
            var correlation = await _dbHelper.GetOrCreateCorrelationAsync(s, logEntry.CorrelationId);
            return await correlation.OnSuccessAsync(async cor =>
            {
                var createdLog = await _dbHelper.CreateLogEntryAsync(cor, logEntry, null);
                return createdLog;
            });
        });
    }

    public async ValueTask<OperationResult> AddAsync<T>(LogEntry<T> logEntry)
    {
        var story = _dbHelper.GetOrCreateStory(logEntry.StoryId);
        return await story.OnSuccessAsync(async s =>
        {
            var correlation = await _dbHelper.GetOrCreateCorrelationAsync(s, logEntry.CorrelationId);
            return await correlation.OnSuccessAsync(async cor =>
            {
                var jsonData = _jsonSerializer.Serialize(logEntry.Data);
                return await jsonData.OnSuccessAsync(async json =>
                {
                    var createdLog = await _dbHelper.CreateLogEntryAsync(cor, logEntry, json);
                    return createdLog;
                });
            });
        });
    }
}
