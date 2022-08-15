using EasySoft.UtilityTools.Result;

namespace EasySoft.UtilityTools.ExtensionMethods;

public static class ExecutiveResultExtensions
{
    public static ExecutiveResult ToMessage(this ExecutiveResult executiveResult, string message)
    {
        executiveResult.Message = message;

        return executiveResult;
    }
}