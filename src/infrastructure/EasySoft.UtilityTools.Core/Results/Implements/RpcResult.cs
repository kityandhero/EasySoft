using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.UtilityTools.Core.Results.Implements;

/// <inheritdoc />
public class RpcResult<T> : IRpcResult<T>
{
    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public bool Success { get; set; }

    /// <inheritdoc />
    public string Message { get; set; } = "";

    /// <inheritdoc />
    public T? Data { get; set; }
}