namespace FlowCode;

public static class OperationResultWhenExtensions
{
    public static OperationResult When(this OperationResult operationResult, Func<OperationResult, bool> condition, Action<OperationResult> action)
    {
        if (condition(operationResult))
        {
            action(operationResult);
        }
        return operationResult;
    }

    public static ValueTask<OperationResult> WhenAsync(this OperationResult operationResult, Func<OperationResult, bool> condition, Func<OperationResult, ValueTask<OperationResult>> action)
    {
        if (condition(operationResult))
        {
            return action(operationResult);
        }
        return ValueTask.FromResult(operationResult);
    }

    public static OperationResult<T> When<T>(this OperationResult<T> operationResult, Func<OperationResult<T>, bool> condition, Action<OperationResult<T>> action)
    {
        if (condition(operationResult))
        {
            action(operationResult);
        }
        return operationResult;
    }

    public static ValueTask<OperationResult<T>> WhenAsync<T>(this OperationResult<T> operationResult, Func<OperationResult<T>, bool> condition, Func<OperationResult<T>, ValueTask<OperationResult<T>>> action)
    {
        if (condition(operationResult))
        {
            return action(operationResult);
        }
        return ValueTask.FromResult(operationResult);
    }
}
