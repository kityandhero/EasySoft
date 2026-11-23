namespace EasySoft.UtilityTools.Core.Assists;

/// <summary>
/// LoggerFactoryAssist
/// </summary>
public static class LoggerFactoryAssist
{
    private static ILoggerFactory _loggerFactory;

    /// <summary>
    /// SetLoggerFactory
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="Exception"></exception>
    public static void SetLoggerFactory(ILoggerFactory logger)
    {
        if (_loggerFactory != null)
        {
            throw new Exception("logger has been set, it disallow set more than once.");
        }

        _loggerFactory = logger;
    }

    /// <summary>
    /// GetLoggerFactory
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ILoggerFactory GetLoggerFactory()
    {
        if (_loggerFactory == null)
        {
            throw new Exception("logger has not set yet");
        }

        return _loggerFactory;
    }
}