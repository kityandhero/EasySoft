namespace EasySoft.Core.Infrastructure.Startup;

/// <summary>
/// StartupMessage
/// </summary>
public class StartupMessage : IStartupMessage
{
    private string _message;

    private bool _extraNewLine;

    private string _extra;

    /// <summary>
    /// Level
    /// </summary>
    public LogLevel Level { get; set; }

    /// <summary>
    /// StartupMessage
    /// </summary>
    public StartupMessage()
    {
        Level = LogLevel.Information;
        _message = "";
        _extraNewLine = false;
        _extra = "";
    }

    /// <summary>
    /// SetLevel
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public IStartupMessage SetLevel(LogLevel level)
    {
        Level = level;

        return this;
    }

    /// <summary>
    /// SetMessage
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public IStartupMessage SetMessage(string message)
    {
        _message = message;

        return this;
    }

    /// <summary>
    /// GetMessage
    /// </summary>
    /// <returns></returns>
    public string GetMessage()
    {
        return _message;
    }

    /// <summary>
    /// SetExtra
    /// </summary>
    /// <param name="extra"></param>
    /// <returns></returns>
    public IStartupMessage SetExtra(string extra)
    {
        _extra = extra;

        return this;
    }

    /// <summary>
    /// GetExtra
    /// </summary>
    /// <returns></returns>
    public string GetExtra()
    {
        return _extra;
    }

    /// <summary>
    /// SetExtraNewLie
    /// </summary>
    /// <param name="newLine"></param>
    /// <returns></returns>
    public IStartupMessage SetExtraNewLie(bool newLine)
    {
        _extraNewLine = newLine;

        return this;
    }

    /// <summary>
    /// GetExtraNewLie
    /// </summary>
    /// <returns></returns>
    public bool GetExtraNewLie()
    {
        return _extraNewLine;
    }
}