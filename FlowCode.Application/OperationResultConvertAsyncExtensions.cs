namespace FlowCode;

public static class OperationResultConvertAsyncExtensions
{
    /// <summary>
    /// Converts an <see cref="OperationResult"/> to a <see cref="ValueTask{Result}"/> using the provided success and error functions.
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="success"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ValueTask<Result> ConvertAsync<Result, T>(this OperationResult<T> operationResult, Func<T, Result> success, Func<Exception, Result> error)
    {
        if (operationResult.IsSuccess)
        {
            if (operationResult.Data is null)
            {
                return ValueTask.FromResult(error(new OperationResultException("Data is null")));
            }

            return ValueTask.FromResult(success(operationResult.Data));
        }

        if (operationResult.Exception is null)
        {
            return ValueTask.FromResult(error(new OperationResultException("Exception is null")));
        }

        return ValueTask.FromResult(error(operationResult.Exception));
    }
    /// <summary>
    /// Converts an <see cref="OperationResult"/> to a <see cref="ValueTask{Result}"/> using the provided success and error functions.
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="success"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ValueTask<Result> ConvertAsync<Result>(this OperationResult operationResult, Func<Result> success, Func<Exception, Result> error)
    {
        if (operationResult.IsSuccess)
        {

            return ValueTask.FromResult(success());
        }

        if (operationResult.Exception is null)
        {
            return ValueTask.FromResult(error(new OperationResultException("Exception is null")));
        }

        return ValueTask.FromResult(error(operationResult.Exception));
    }
}

