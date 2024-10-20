using FlowCode;

namespace Captinslog.Infrastructure;

public interface IJsonSerializer
{
    OperationResult<string> Serialize<T>(T obj);
    OperationResult<T> Deserialize<T>(string json);
}
