namespace EasySoft.Core.Dapper.Elegant.Configure;

public static class DapperElegantConfigurator
{
    private static ICacheOperator? _cacheOperator;

    public static void SetCacheOperator(ICacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    public static ICacheOperator? GetCacheOperator()
    {
        return _cacheOperator;
    }
}