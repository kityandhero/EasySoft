using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Startup;

public class StartupMessage : IStartupMessage
{
    private string _message;

    private bool _extraNewLine;

    private string _extra;

    public LogLevel Level { get; set; }

    public StartupMessage()
    {
        Level = LogLevel.Information;
        _message = "";
        _extraNewLine = false;
        _extra = "";
    }

    public IStartupMessage SetLevel(LogLevel level)
    {
        Level = level;

        return this;
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

    public IStartupMessage SetExtraNewLie(bool newLine)
    {
        _extraNewLine = newLine;

        return this;
    }

    public bool GetExtraNewLie()
    {
        return _extraNewLine;
    }
}