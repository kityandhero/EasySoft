using EasySoft.UtilityTools.Core.Results.Factories;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// ExecutiveResultExtensions
/// </summary>
public static class ExecutiveResultExtensions
{
    /// <summary>
    /// ToExecutiveResult
    /// </summary>
    /// <param name="executiveResult"></param>
    /// <returns></returns>
    public static RpcResult<T> ToRpcResult<T>(this ExecutiveResult<T> executiveResult)
    {
        return RpcResultFactory.CreateFromReturnMessage(executiveResult.Code, executiveResult.Data);
    }

    /// <summary>
    /// ToExecutiveResult
    /// </summary>
    /// <param name="executiveResult"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static RpcResult<T> ToRpcResult<T>(this ExecutiveResult<T> executiveResult, T? data)
    {
        return RpcResultFactory.CreateFromReturnMessage(executiveResult.Code, data);
    }
}