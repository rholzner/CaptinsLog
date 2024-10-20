namespace FlowCode;
public static class OperationResultOnSuccessExtensions
{

    /// <summary>
    /// Executes the provided action if the operation result is successful.
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static OperationResult OnSuccess(this OperationResult operationResult, Func<OperationResult> action)
    {
        if (operationResult.IsSuccess)
        {
            return action();
        }

        if (operationResult.Exception is null)
        {
            return new OperationResultException("Exception is null");
        }

        return operationResult.Exception;
    }

    /// <summary>
    /// Executes the provided action if the operation result is successful.
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static OperationResult<Result> OnSuccess<Result>(this OperationResult operationResult, Func<Result> action)
    {
        if (operationResult.IsSuccess)
        {
            return action();
        }

        if (operationResult.Exception is null)
        {
            return new OperationResultException("Exception is null");
        }

        return operationResult.Exception;
    }

    /// <summary>
    /// Executes the provided action if the operation result is successful.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Result"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static OperationResult<Result> OnSuccess<T, Result>(this OperationResult<T> operationResult, Func<T, OperationResult<Result>> action)
    {
        if (operationResult.IsSuccess)
        {
            return action(operationResult.Data);
        }

        if (operationResult.Exception is null)
        {
            return new OperationResultException("Exception is null");
        }

        return operationResult.Exception;
    }

    public static OperationResult OnSuccess<T>(this OperationResult<T> operationResult, Func<T, OperationResult> action)
    {
        if (operationResult.IsSuccess)
        {
            return action(operationResult.Data);
        }

        if (operationResult.Exception is null)
        {
            return new OperationResultException("Exception is null");
        }

        return operationResult.Exception;
    }
}
