namespace FlowCode;

public static class OperationResultConvertExtensions
{
    /// <summary>
    /// Converts an <see cref="OperationResult{T}"/> to a <see cref="Result"/> using the provided functions.
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="success"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result Convert<Result, T>(this OperationResult<T> operationResult, Func<T, Result> success, Func<Exception, Result> error)
    {
        if (operationResult.IsSuccess)
        {
            if (operationResult.Data is null)
            {
                return error(new OperationResultException("Data is null"));
            }

            return success(operationResult.Data);
        }

        if (operationResult.Exception is null)
        {
            return error(new OperationResultException("Exception is null"));
        }

        return error(operationResult.Exception);
    }
}

