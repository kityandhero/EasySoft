using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

public static class ExecutiveResultExtensions
{
    public static ExecutiveResult ToMessage(this ExecutiveResult executiveResult, string message)
    {
        executiveResult.Message = message;

        return executiveResult;
    }
}