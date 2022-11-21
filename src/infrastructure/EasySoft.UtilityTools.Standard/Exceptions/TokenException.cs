namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// 凭据异常
/// </summary>
public class TokenException : Exception
{
    /// <summary>
    /// 凭据异常
    /// </summary>
    public TokenException() : this("")
    {
    }

    /// <summary>
    /// 凭据异常
    /// </summary>
    /// <param name="message"></param>
    public TokenException(string message) : base(message)
    {
    }
}