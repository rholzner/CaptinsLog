namespace FlowCode;

public class OperationResults : List<OperationResult>
{
    public OperationResults()
    {
    }
    public OperationResults(params IEnumerable<OperationResult> operationResults) : base(operationResults)
    {
    }

    /// <summary>
    /// Adds the operation results.
    /// </summary>
    /// <param name="operationResults"></param>
    /// <returns></returns>
    public OperationResults Add(params IEnumerable<OperationResult> operationResults)
    {
        AddRange(operationResults);
        return this;
    }

    /// <summary>
    /// Adds the operation results that meet the condition.
    /// </summary>
    /// <param name="addCondition"></param>
    /// <param name="operationResults"></param>
    /// <returns></returns>
    public OperationResults Add(Func<OperationResult, bool> addCondition, params IEnumerable<OperationResult> operationResults)
    {
        foreach (var operationResult in operationResults)
        {
            if (addCondition(operationResult))
            {
                Add(operationResult);
            }
        }
        return this;
    }

    public bool IsSuccess => this.All(x => x.IsSuccess);
}
