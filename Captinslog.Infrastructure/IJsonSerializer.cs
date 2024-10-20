using FlowCode;

namespace Captinslog.Infrastructure;

public interface IJsonSerializer
{
    OperationResult<string> Serialize<T>(T obj);
    OperationResult<T> Deserialize<T>(string json);
}

public class JsonSerializer : IJsonSerializer
{
    public OperationResult<string> Serialize<T>(T obj)
    {
        try
        {
            var json = System.Text.Json.JsonSerializer.Serialize(obj);
            return OperationResult<string>.Success(json);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    public OperationResult<T> Deserialize<T>(string json)
    {
        try
        {
            var obj = System.Text.Json.JsonSerializer.Deserialize<T>(json);
            return OperationResult<T>.Success(obj);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}