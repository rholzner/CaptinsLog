using Captinslog.Domain;
using Captinslog.Infrastructure.Entities;
using FlowCode;
using Microsoft.EntityFrameworkCore;

namespace Captinslog.Infrastructure;

public interface ILogEntryDatabaseContextHelper
{
    CorrelationEntity CreateNewCorrelation(StoryEntity story, Guid correlationId);
    OperationResult<CorrelationEntity> GetOrCreateCorrelation(StoryEntity story, Guid correlationId);
    ValueTask<OperationResult<CorrelationEntity>> GetOrCreateCorrelationAsync(StoryEntity story, Guid correlationId);
    OperationResult<StoryEntity> GetOrCreateStory(Guid storyId);
    ValueTask<OperationResult<StoryEntity>> GetOrCreateStoryAsync(Guid storyId);
    OperationResult<LogEntryEntity> CreateLogEntry(CorrelationEntity correlationEntity, LogEntry logEntry,string? json);
    ValueTask<OperationResult<LogEntryEntity>> CreateLogEntryAsync(CorrelationEntity correlationEntity, LogEntry logEntry,string? json);
}

public class LogEntryDatabaseContextHelper : ILogEntryDatabaseContextHelper
{
    private readonly LogEntryDatabaseContext _db;

    public LogEntryDatabaseContextHelper(LogEntryDatabaseContext logEntryDatabaseContext)
    {
        _db = logEntryDatabaseContext;
    }
    public OperationResult<StoryEntity> GetOrCreateStory(Guid storyId)
    {
        var story = _db.Stories.FirstOrDefault(x => x.StoryId == storyId);
        if (story is not null)
        {
            return story;
        }

        story = CreateNewStory(storyId);
        _db.Stories.Add(story);

        try
        {
            _db.SaveChanges();
            return story;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async ValueTask<OperationResult<StoryEntity>> GetOrCreateStoryAsync(Guid storyId)
    {
        var story = await _db.Stories.FirstOrDefaultAsync(x => x.StoryId == storyId);
        if (story is not null)
        {
            return story;
        }

        story = CreateNewStory(storyId);
        _db.Stories.Add(story);

        try
        {
            await _db.SaveChangesAsync();
            return story;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    private static StoryEntity CreateNewStory(Guid storyId)
    {
        return new StoryEntity
        {
            StoryId = storyId,
            Correlations = new List<CorrelationEntity>()
        };
    }


    public OperationResult<CorrelationEntity> GetOrCreateCorrelation(StoryEntity story, Guid correlationId)
    {
        var correlation = _db.Correlations.FirstOrDefault(x => x.CorrelationId == correlationId);
        if (correlation is not null)
        {
            return correlation;
        }
        correlation = CreateNewCorrelation(story, correlationId);

        _db.Correlations.Add(correlation);

        try
        {
            _db.SaveChanges();
            return correlation;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async ValueTask<OperationResult<CorrelationEntity>> GetOrCreateCorrelationAsync(StoryEntity story, Guid correlationId)
    {
        var correlation = await _db.Correlations.FirstOrDefaultAsync(x => x.CorrelationId == correlationId);
        if (correlation is not null)
        {
            return correlation;
        }
        correlation = CreateNewCorrelation(story, correlationId);
        _db.Correlations.Add(correlation);
        try
        {
            await _db.SaveChangesAsync();
            return correlation;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public CorrelationEntity CreateNewCorrelation(StoryEntity story, Guid correlationId)
    {
        return new CorrelationEntity
        {
            CorrelationId = correlationId,
            Story = story,
            StoryId = story.StoryId,
        };
    }

    public OperationResult<LogEntryEntity> CreateLogEntry(CorrelationEntity correlationEntity, LogEntry logEntry,string? json)
    {
        var logEntryEntity = AddNewLogEntry(logEntry, correlationEntity, json);
        
        try
        {
            _db.SaveChanges();
            return logEntryEntity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    private LogEntryEntity AddNewLogEntry(LogEntry logEntry, CorrelationEntity correlationEntity, string? json)
    {
        var logEntryEntity = new LogEntryEntity
        {
            Correlation = correlationEntity,
            CorrelationId = correlationEntity.Id,
            Created = logEntry.Date,
            Message = logEntry.Message,
            IsSuccess = logEntry.IsSuccess,
            Data = json,
        };
        _db.LogEntries.Add(logEntryEntity);

        return logEntryEntity;
    }

    public async ValueTask<OperationResult<LogEntryEntity>> CreateLogEntryAsync(CorrelationEntity correlationEntity, LogEntry logEntry, string? json)
    {
        var logEntryEntity = AddNewLogEntry(logEntry, correlationEntity, json);

        try
        {
            await _db.SaveChangesAsync();
            return logEntryEntity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
