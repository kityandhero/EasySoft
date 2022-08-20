using System.ComponentModel;

namespace EasySoft.Core.EasyCaching.Enums;

[Description("内存模式")]
public enum CacheModeCollection
{
    [Description("内存缓存")]
    InMemory = 100,

    [Description("Redis缓存")]
    Redis = 200,
}