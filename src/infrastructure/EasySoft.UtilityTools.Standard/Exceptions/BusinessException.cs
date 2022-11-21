namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// BusinessException
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// BusinessException
    /// </summary>
    public BusinessException() : this("")
    {
    }

    /// <summary>
    /// BusinessException
    /// </summary>
    /// <param name="message"></param>
    public BusinessException(string message) : base(message)
    {
    }
}