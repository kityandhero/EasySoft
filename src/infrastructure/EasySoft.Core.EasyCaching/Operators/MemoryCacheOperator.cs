using EasyCaching.Core;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.Result;

namespace EasySoft.Core.EasyCaching.Operators;

public class MemoryCacheOperator : GeneralCacheOperator
{
    public MemoryCacheOperator(IEasyCachingProvider provider) : base(provider)
    {
    }
}