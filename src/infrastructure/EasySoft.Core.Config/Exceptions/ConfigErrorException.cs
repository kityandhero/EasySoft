namespace EasySoft.Core.Config.Exceptions;

public class ConfigErrorException : Exception
{
    public ConfigErrorException() : this("config error")
    {
    }

    public ConfigErrorException(string message) : base(message)
    {
    }

    public ConfigErrorException(string message, string fileInfo) : base(
        $"{message}{(string.IsNullOrWhiteSpace(fileInfo) ? "" : $", config file is {fileInfo}")}"
    )
    {
    }
}