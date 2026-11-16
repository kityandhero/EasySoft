namespace EasySoft.UtilityTools.Standard.Exceptions;

/// <summary>
/// ConfigErrorException
/// </summary>
public class ConfigErrorException : Exception
{
    /// <summary>
    /// ConfigErrorException
    /// </summary>
    public ConfigErrorException() : this("config error")
    {
    }

    /// <summary>
    /// ConfigErrorException
    /// </summary>
    /// <param name="message"></param>
    public ConfigErrorException(string message) : base(message)
    {
    }
}