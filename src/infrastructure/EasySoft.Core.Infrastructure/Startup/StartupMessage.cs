using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Startup;

public class StartupMessage : IStartupMessage
{
    private LogLevel _logLevel;

    private string _message;

    private string _extra;

    public StartupMessage()
    {
        _logLevel = LogLevel.Information;
        _message = "";
        _extra = "";
    }

    public IStartupMessage SetLevel(LogLevel level)
    {
        _logLevel = level;

        return this;
    }

    public LogLevel GetLevel()
    {
        return _logLevel;
    }

    public IStartupMessage SetMessage(string message)
    {
        _message = message;

        return this;
    }

    public string GetMessage()
    {
        return _message;
    }

    public IStartupMessage SetExtra(string extra)
    {
        _extra = extra;

        return this;
    }

    public string GetExtra()
    {
        return _extra;
    }
}