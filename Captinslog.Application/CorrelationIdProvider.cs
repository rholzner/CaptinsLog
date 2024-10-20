using FlowCode;

namespace Captinslog.Application;
public interface ICorrelationIdProvider
{
    /// <summary>
    /// Should be scoped to the lifetime of a request
    /// Gets or creates a correlation id for the current context/request
    /// </summary>
    /// <returns></returns>
    OperationResult<Guid> BeginScope();
}
/// <summary>
/// Should be scoped to the lifetime of a request and disposed at the end of the request
/// </summary>
public class CorrelationIdProvider : ICorrelationIdProvider, IDisposable
{
    public Guid _correlationId;
    public OperationResult<Guid> BeginScope()
    {
        if (_correlationId != Guid.Empty)
        {
            return _correlationId;
        }
        _correlationId = Guid.NewGuid();
        return _correlationId;
    }
    public void Dispose()
    {
        _correlationId = Guid.Empty;
    }
}