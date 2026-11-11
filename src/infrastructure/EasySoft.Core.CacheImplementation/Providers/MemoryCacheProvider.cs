using Microsoft.Extensions.Caching.Memory;

namespace EasySoft.Core.CacheImplementation.Providers;

/// <summary>
/// MemoryCacheProvider
/// </summary>
internal class MemoryCacheProvider
{
    private MemoryCacheProvider()
    {
        var p = new MemoryCache(new MemoryCacheOptions());
    }

    public static MemoryCache GetInstance()
    {
        return SingleHelper.GetSingletonQuery();
    }

    private static class SingleHelper
    {
        private static readonly MemoryCache Cache = new(new MemoryCacheOptions());

        public static MemoryCache GetSingletonQuery()
        {
            return Cache;
        }
    }
}