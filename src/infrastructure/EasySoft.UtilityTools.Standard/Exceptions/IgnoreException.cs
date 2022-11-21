namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// 忽略异常
/// </summary>
public class IgnoreException : Exception
{
    /// <summary>
    /// 忽略异常
    /// </summary>
    public IgnoreException() : this("")
    {
    }

    /// <summary>
    /// 忽略异常
    /// </summary>
    /// <param name="message"></param>
    public IgnoreException(string message) : base(message)
    {
    }
}