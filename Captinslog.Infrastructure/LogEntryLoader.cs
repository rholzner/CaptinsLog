using Captinslog.Domain;
using FlowCode;
using Microsoft.EntityFrameworkCore;

namespace Captinslog.Infrastructure;

public class LogEntryLoader : ILogEntryLoader
{
    private readonly LogEntryDatabaseContext _db;

    public LogEntryLoader(LogEntryDatabaseContext logEntryDatabaseContext)
    {
        _db = logEntryDatabaseContext;
    }

    public async ValueTask<OperationResult<LogEntry>> Get(Guid logEntryId)
    {
        try
        {
            var data = await _db.LogEntries.Include(x => x.Correlation).ThenInclude(x => x.Story).FirstOrDefaultAsync(x => x.Id == logEntryId);
            if (data is not null)
            {
                return data.ToLogEntry();
            }

            return new OperationResult<LogEntry>(new LogEntryException("did not find data"));
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllAsync(int skip, int take)
    {
        try
        {
            var data = await _db.LogEntries.Include(x => x.Correlation).ThenInclude(x => x.Story).Skip(skip).Take(take).ToArrayAsync();
            var r = data.Select(x => x.ToLogEntry());

            return OperationResult<IEnumerable<LogEntry>>.Success(r);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInCorrelationId(Guid correlationId)
    {
        try
        {
            var data = await _db.LogEntries.Include(x => x.Correlation).ThenInclude(x => x.Story).Where(x => x.Correlation.CorrelationId == correlationId).ToArrayAsync();
            var r = data.Select(x => x.ToLogEntry());

            return OperationResult<IEnumerable<LogEntry>>.Success(r);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInStory(Guid storyId)
    {
        try
        {
            var data = await _db.LogEntries.Include(x => x.Correlation).ThenInclude(x => x.Story).Where(x => x.Correlation.Story.StoryId == storyId).ToArrayAsync();
            var r = data.Select(x => x.ToLogEntry());
            
            return OperationResult<IEnumerable<LogEntry>>.Success(r);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
