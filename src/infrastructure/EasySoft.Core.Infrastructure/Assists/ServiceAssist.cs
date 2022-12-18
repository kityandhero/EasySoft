namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// ServiceAssist
/// </summary>
public static class ServiceAssist
{
    private static IServiceProvider? _serviceProvider;

    /// <summary>
    /// SetServiceProvider
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="Exception"></exception>
    public static void SetServiceProvider(IServiceProvider? serviceProvider)
    {
        if (_serviceProvider != null)
            throw new Exception("serviceProvider has been set, it disallow set more than once.");

        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// GetServiceProvider
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IServiceProvider GetServiceProvider()
    {
        if (_serviceProvider == null) throw new Exception("serviceProvider has not set yet");

        return _serviceProvider;
    }

    /// <summary>
    /// GetServerPath
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetServerPath(string path)
    {
        return GetServiceProvider().GetRequiredService<IWebHostEnvironment>().ContentRootPath + path;
    }
}