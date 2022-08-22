using EasyCaching.Core;

namespace EasySoft.Core.EasyCaching.Operators;

public class RedisCacheOperator : GeneralCacheOperator
{
    public RedisCacheOperator(IEasyCachingProvider provider) : base(provider)
    {
    }
}