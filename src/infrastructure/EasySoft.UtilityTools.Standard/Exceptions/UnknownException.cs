using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// 未知的错误
/// </summary>
public class UnknownException : Exception
{
    /// <summary>
    /// UnknownException
    /// </summary>
    /// <param name="message"></param>
    public UnknownException(string message = "") : base("未知的错误{0}".FormatValue(message))
    {
    }
}