using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Startup;

public interface IStartupMessage
{
    public IStartupMessage SetLevel(LogLevel level);

    public LogLevel GetLevel();

    public IStartupMessage SetMessage(string message);

    public string GetMessage();

    public IStartupMessage SetExtra(string extra);

    public string GetExtra();
}