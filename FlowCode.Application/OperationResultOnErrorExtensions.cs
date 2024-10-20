namespace FlowCode;

public static class OperationResultOnErrorExtensions
{
    /// <summary>
    /// Executes the provided action if the operation result is not successful.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static OperationResult<T> OnError<T>(this OperationResult<T> operationResult, Action<Exception> action)
    {
        if (!operationResult.IsSuccess)
        {
            if (operationResult.Exception is null)
            {
                return new OperationResultException("Exception is null");
            }
            action(operationResult.Exception);
        }
        return operationResult;
    }

    public static OperationResult<T> OnError<T>(this OperationResult<T> operationResult, Action<Exception,T> action)
    {
        if (!operationResult.IsSuccess)
        {
            if (operationResult.Exception is null)
            {
                return new OperationResultException("Exception is null");
            }
            action(operationResult.Exception, operationResult.Data);
        }
        return operationResult;
    }


    /// <summary>
    /// Executes the provided action if the operation result is not successful.
    /// </summary>
    /// <param name="operationResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static OperationResult OnError(this OperationResult operationResult, Action<Exception> action)
    {
        if (!operationResult.IsSuccess)
        {
            if (operationResult.Exception is null)
            {
                return new OperationResultException("Exception is null");
            }

            action(operationResult.Exception);
        }
        return operationResult;
    }

    /// <summary>
    /// Executes the provided action if the operation result is not successful.
    /// </summary>
    /// <typeparam name="Result"></typeparam>
    /// <param name="operationResult"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static OperationResult<Result> OnError<Result>(this OperationResult operationResult, Func<Exception, Result> action)
    {
        if (!operationResult.IsSuccess)
        {
            if (operationResult.Exception is null)
            {
                return new OperationResultException("Exception is null");
            }
            return action(operationResult.Exception);
        }

        if (operationResult.IsSuccess)
        {
            return default;
        }

        if (operationResult.Exception is null)
        {
            return new OperationResultException("Exception is null");
        }

        return operationResult.Exception;
    }

    public static OperationResult<Result> OnError<T,Result>(this OperationResult<T> operationResult, Func<Exception, Result> action)
    {
        if (!operationResult.IsSuccess)
        {
            if (operationResult.Exception is null)
            {
                return new OperationResultException("Exception is null");
            }
            return action(operationResult.Exception);
        }
        else if(operationResult.Data is Result old)
        {
            return old;
        }

        if (operationResult.Exception is null)
        {
            return new OperationResultException("Exception is null");
        }

        return operationResult.Exception;
    }
}
