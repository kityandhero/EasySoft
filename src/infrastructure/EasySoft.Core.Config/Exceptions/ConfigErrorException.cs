namespace EasySoft.Core.Config.Exceptions;

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

    /// <summary>
    /// ConfigErrorException
    /// </summary>
    /// <param name="message"></param>
    /// <param name="fileInfo"></param>
    public ConfigErrorException(string message, string fileInfo) : base(
        $"{message}{(string.IsNullOrWhiteSpace(fileInfo) ? "" : $", config file is {fileInfo}")}"
    )
    {
    }
}