namespace EasySoft.Core.Infrastructure.Assists;

public static class ServiceAssist
{
    private static IServiceProvider? _serviceProvider;

    public static void SetServiceProvider(IServiceProvider? serviceProvider)
    {
        if (_serviceProvider != null)
        {
            throw new Exception("serviceProvider has been set, it disallow set more than once.");
        }

        _serviceProvider = serviceProvider;
    }

    public static IServiceProvider? GetServiceProvider()
    {
        if (_serviceProvider == null)
        {
            throw new Exception("serviceProvider has not set yet");
        }

        return _serviceProvider;
    }
}