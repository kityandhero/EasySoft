using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Core.Results.Factories;

/// <summary>
/// rpc result factory
/// </summary>
public static class RpcResultFactory
{
    /// <summary>
    /// create success result
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static RpcResult<T> CreateSuccess<T>(T? data)
    {
        return new RpcResult<T>()
        {
            Code = ReturnCode.Ok.ToInt(),
            Success = true,
            Data = data
        };
    }

    /// <summary>
    /// create success result
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static RpcResult<T> CreateFail<T>(ReturnCode code, string message)
    {
        return new RpcResult<T>
        {
            Code = code.ToInt(),
            Success = false,
            Message = message,
            Data = default
        };
    }

    /// <summary>
    /// create success result
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static RpcResult<T> CreateFail<T>(ReturnCode code, string message, T? data)
    {
        return new RpcResult<T>
        {
            Code = code.ToInt(),
            Success = false,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// create success result
    /// </summary>
    /// <param name="returnMessage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static RpcResult<T> CreateFromReturnMessage<T>(ReturnMessage returnMessage)
    {
        return new RpcResult<T>
        {
            Code = returnMessage.Code,
            Success = returnMessage.Success,
            Message = returnMessage.Message,
            Data = default
        };
    }

    /// <summary>
    /// create success result
    /// </summary>
    /// <param name="returnMessage"></param>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static RpcResult<T> CreateFromReturnMessage<T>(ReturnMessage returnMessage, T? data)
    {
        return new RpcResult<T>
        {
            Code = returnMessage.Code,
            Success = returnMessage.Success,
            Message = returnMessage.Message,
            Data = data
        };
    }
}