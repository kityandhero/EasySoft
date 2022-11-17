namespace EasySoft.Core.Infrastructure.Startup;

/// <summary>
/// IStartupMessage
/// </summary>
public interface IStartupMessage
{
    /// <summary>
    /// Level
    /// </summary>
    public LogLevel Level { get; set; }

    /// <summary>
    /// SetLevel
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public IStartupMessage SetLevel(LogLevel level);

    /// <summary>
    /// SetMessage
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public IStartupMessage SetMessage(string message);

    /// <summary>
    /// GetMessage
    /// </summary>
    /// <returns></returns>
    public string GetMessage();

    /// <summary>
    /// SetExtra
    /// </summary>
    /// <param name="extra"></param>
    /// <returns></returns>
    public IStartupMessage SetExtra(string extra);

    /// <summary>
    /// GetExtra
    /// </summary>
    /// <returns></returns>
    public string GetExtra();

    /// <summary>
    /// SetExtraNewLie
    /// </summary>
    /// <param name="newLine"></param>
    /// <returns></returns>
    public IStartupMessage SetExtraNewLie(bool newLine);

    /// <summary>
    /// GetExtraNewLie
    /// </summary>
    /// <returns></returns>
    public bool GetExtraNewLie();
}