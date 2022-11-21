using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// 不支持异常
/// </summary>
public class UnsupportedException : Exception
{
    /// <summary>
    /// UnsupportedException
    /// </summary>
    /// <param name="message"></param>
    public UnsupportedException(string message = "") : base("Unsupported:{0}".FormatValue(message))
    {
    }
}