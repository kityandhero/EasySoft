namespace EasySoft.Core.Config.Exceptions;

/// <summary>
/// ConfigErrorException
/// </summary>
public class ConfigFileErrorException : Exception
{
    /// <summary>
    /// ConfigErrorException
    /// </summary>
    public ConfigFileErrorException() : this("config error")
    {
    }

    /// <summary>
    /// ConfigErrorException
    /// </summary>
    /// <param name="message"></param>
    public ConfigFileErrorException(string message) : base(message)
    {
    }

    /// <summary>
    /// ConfigErrorException
    /// </summary>
    /// <param name="message"></param>
    /// <param name="fileInfo"></param>
    public ConfigFileErrorException(string message, string fileInfo) : base(
        $"{message}{(string.IsNullOrWhiteSpace(fileInfo) ? "" : $", config file is {fileInfo}")}"
    )
    {
    }
}