using EasySoft.Core.CacheCore.Enums;
using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.CacheCore.Models;

public class CacheKeyValue<T>
{
    /// <summary>
    /// 缓存键名
    /// </summary>
    public string Key { get; set; } = "";

    /// <summary>
    /// 结果值, 来自数据源或缓存
    /// </summary>
    public T? Value { get; set; } = default;

    /// <summary>
    /// 设置缓存值时的值序列化结果, 仅在设置或刷新缓存时有值, 值仅来自缓存时为空白
    /// </summary>
    public string SerializationSource { get; set; } = "";

    /// <summary>
    /// 缓存中的值字符串
    /// </summary>
    public string CacheString { get; set; } = "";

    /// <summary>
    /// 强制更新
    /// </summary>
    public bool ForceRefresh { get; set; } = false;

    /// <summary>
    /// 值的来源
    /// </summary>
    public int ValueSourceType { get; set; } = ValueSource.Other.ToInt();

    /// <summary>
    /// 值的来源
    /// </summary>
    public string ValueSourceNote => ValueSourceType.GetEnumDescription<ValueSource>();
}