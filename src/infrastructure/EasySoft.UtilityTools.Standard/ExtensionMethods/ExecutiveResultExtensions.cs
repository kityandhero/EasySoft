using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

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
}