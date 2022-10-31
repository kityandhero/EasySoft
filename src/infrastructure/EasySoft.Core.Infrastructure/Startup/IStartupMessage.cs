using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Startup;

public interface IStartupMessage
{
    public LogLevel Level { get; set; }

    public IStartupMessage SetLevel(LogLevel level);

    public IStartupMessage SetMessage(string message);

    public string GetMessage();

    public IStartupMessage SetExtra(string extra);

    public string GetExtra();

    public IStartupMessage SetExtraNewLie(bool newLine);

    public bool GetExtraNewLie();
}