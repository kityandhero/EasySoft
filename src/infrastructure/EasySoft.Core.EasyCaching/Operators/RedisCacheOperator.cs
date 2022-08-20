using EasyCaching.Core;
using EasySoft.UtilityTools.Assists;
using EasySoft.UtilityTools.Result;

namespace EasySoft.Core.EasyCaching.Operators;

public class RedisCacheOperator : GeneralCacheOperator
{
    public RedisCacheOperator(IEasyCachingProvider provider) : base(provider)
    {
    }
}