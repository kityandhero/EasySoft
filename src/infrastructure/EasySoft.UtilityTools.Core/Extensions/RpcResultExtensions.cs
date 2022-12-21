using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// RpcResultExtensions
/// </summary>
public static class RpcResultExtensions
{
    /// <summary>
    /// ToExecutiveResult
    /// </summary>
    /// <param name="rpcResult"></param>
    /// <returns></returns>
    public static ReturnMessage GetReturnMessage<T>(this RpcResult<T> rpcResult)
    {
        return new ReturnMessage(
            rpcResult.Code,
            rpcResult.Message,
            rpcResult.Success
        );
    }

    /// <summary>
    /// ToExecutiveResult
    /// </summary>
    /// <param name="rpcResult"></param>
    /// <returns></returns>
    public static ExecutiveResult<T> ToExecutiveResult<T>(this RpcResult<T> rpcResult)
    {
        return new ExecutiveResult<T>(rpcResult.GetReturnMessage())
        {
            Data = rpcResult.Data
        };
    }
}