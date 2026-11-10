namespace EasySoft.Core.Dapper.Elegant.Configure;

public static class DapperElegantConfigurator
{
    private static IAsyncCacheOperator _cacheOperator;

    public static void SetCacheOperator(IAsyncCacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    public static IAsyncCacheOperator GetCacheOperator()
    {
        return _cacheOperator;
    }
}