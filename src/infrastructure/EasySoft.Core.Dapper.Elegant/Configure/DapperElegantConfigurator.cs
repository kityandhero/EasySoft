using EasySoft.Core.CacheCore.interfaces;

namespace EasySoft.Core.Dapper.Elegant.Configure;

public static class DapperElegantConfigurator
{
    private static Func<int, bool> _sqlLogRecordJudge = _ => false;

    private static ICacheOperator? _cacheOperator;

    public static void SetSqlLogRecordJudge(Func<int, bool> sqlLogRecordJudge)
    {
        _sqlLogRecordJudge = sqlLogRecordJudge;
    }

    public static Func<int, bool> GetSqlLogRecordJudge()
    {
        return _sqlLogRecordJudge;
    }

    public static void SetCacheOperator(ICacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    public static ICacheOperator? GetCacheOperator()
    {
        return _cacheOperator;
    }
}