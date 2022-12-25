using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ExecutiveResultExtensions
/// </summary>
public static class ExecutiveResultExtensions
{
    /// <summary>
    /// ToMessage
    /// </summary>
    /// <param name="executiveResult"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ExecutiveResult ToMessage(this ExecutiveResult executiveResult, string message)
    {
        executiveResult.Message = message;

        return executiveResult;
    }

    /// <summary>
    /// ToExecutiveResult
    /// </summary>
    /// <param name="executiveResult"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ExecutiveResult<TR> ToExecutiveResult<T, TR>(this ExecutiveResult<T> executiveResult, TR? data)
    {
        return new ExecutiveResult<TR>(executiveResult.Code)
        {
            Data = data
        };
    }
}