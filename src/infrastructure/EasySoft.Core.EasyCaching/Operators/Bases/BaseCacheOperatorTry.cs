using EasySoft.Core.CacheCore.Enums;
using EasySoft.Core.CacheCore.ExtensionMethods;
using EasySoft.Core.CacheCore.Models;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Result.Implements;
using Newtonsoft.Json;

namespace EasySoft.Core.EasyCaching.Operators.Bases;

public abstract partial class BaseCacheOperator
{
    #region TryGetWithSuperiorStorage

    #region TryGetWithSuperiorStorage<T, TArgument>

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            _ => GetSuperiorCacheExpireTime(),
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            _ => GetSuperiorCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 20 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _) => GetSuperiorCacheExpireTime(),
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _) => GetSuperiorCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2, TArgument3>

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => GetSuperiorCacheExpireTime(),
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => GetSuperiorCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => GetSuperiorCacheExpireTime(),
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => GetSuperiorCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSuperiorStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => GetSuperiorCacheExpireTime(),
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSuperiorStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => GetSuperiorCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #endregion

    #region TryGetWithProvisionalStorage

    #region TryGetWithProvisionalStorage<T, TArgument>

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            _ => GetProvisionalCacheExpireTime(),
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            _ => GetProvisionalCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2>

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _) => GetProvisionalCacheExpireTime(),
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _) => GetProvisionalCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2, TArgument3>

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => GetProvisionalCacheExpireTime(),
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => GetProvisionalCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => GetProvisionalCacheExpireTime(),
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => GetProvisionalCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGetWithProvisionalStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => GetProvisionalCacheExpireTime(),
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithProvisionalStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => GetProvisionalCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #endregion

    #region TryGetWithSteadyStorage

    #region TryGetWithSteadyStorage<T, TArgument>

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            _ => GetSteadyCacheExpireTime(),
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            _ => GetSteadyCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2>

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _) => GetSteadyCacheExpireTime(),
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _) => GetSteadyCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3>

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => GetSteadyCacheExpireTime(),
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (_, _, _) => GetSteadyCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => GetSteadyCacheExpireTime(),
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _
            ) => GetSteadyCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGetWithSteadyStorage(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => GetSteadyCacheExpireTime(),
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGetWithSteadyStorage<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => GetSteadyCacheExpireTime(),
            forceRefreshAction
        );
    }

    #endregion

    #endregion

    #region TryGet

    #region TryGet<T>

    public CacheKeyValue<T> TryGet<T>(
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            () => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            _ => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<T>(
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        Func<T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            () => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGet<T>(
        Func<ExecutiveResult> detectParameterAction,
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            _ => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<T>(
        Func<ExecutiveResult> detectParameterAction,
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        Func<T, bool>? forceRefreshAction
    ) where T : new()
    {
        var resultDetectParameter = detectParameterAction();

        if (!resultDetectParameter.Success)
        {
            throw new UnhandledException(resultDetectParameter.Message);
        }

        if (keyBuildAction == null)
        {
            throw new UnhandledException("键名构建方法无效");
        }

        if (valueBuildAction == null)
        {
            throw new UnhandledException("键值构建方法无效");
        }

        var key = keyBuildAction();

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new UnhandledException("构建缓存键名时，得到了空白值");
        }

        var resultGetSerializedValue = GetSerializedValue(key);

        if (!resultGetSerializedValue.Success)
        {
            throw new UnhandledException(resultGetSerializedValue.Message);
        }

        var v = resultGetSerializedValue.Data ?? "";

        if (!string.IsNullOrWhiteSpace(v))
        {
            try
            {
                var analyzeResult = JsonConvert.DeserializeObject<T>(v);

                if (analyzeResult != null)
                {
                    if (forceRefreshAction == null || !forceRefreshAction(analyzeResult))
                    {
                        return new CacheKeyValue<T>
                        {
                            Key = key,
                            Value = analyzeResult,
                            CacheString = v,
                            ForceRefresh = false,
                            ValueSourceType = ValueSource.Cache.ToInt()
                        };
                    }
                }
            }
            catch (JsonSerializationException e)
            {
                e.Data.Add("json", v);

                throw;
            }
            catch (Exception e)
            {
                e.Data.Add("json", v);
            }
        }

        var value = valueBuildAction();

        var expireTime = expireTimeBuildAction();

        var json = JsonConvert.SerializeObject(value);

        Set(
            key,
            json,
            expireTime
        );

        return new CacheKeyValue<T>
        {
            Key = key,
            Value = value,
            SerializationSource = json,
            CacheString = json,
            ForceRefresh = true,
            ValueSourceType = ValueSource.Source.ToInt()
        };
    }

    #endregion

    #region TryGet<T, TArgument>

    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument,
            _ => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (_, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new()
    {
        var resultDetectParameter = detectParameterAction(argument);

        if (!resultDetectParameter.Success)
        {
            throw new UnhandledException(resultDetectParameter.Message);
        }

        if (keyBuildAction == null)
        {
            throw new UnhandledException("键名构建方法无效");
        }

        if (valueBuildAction == null)
        {
            throw new UnhandledException("键值构建方法无效");
        }

        var key = keyBuildAction(argument);

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new UnhandledException("构建缓存键名时，得到了空白值");
        }

        var resultGetSerializedValue = GetSerializedValue(key);

        if (!resultGetSerializedValue.Success)
        {
            throw new UnhandledException(resultGetSerializedValue.Message);
        }

        var v = resultGetSerializedValue.Data ?? "";

        if (!string.IsNullOrWhiteSpace(v))
        {
            try
            {
                var analyzeResult = JsonConvert.DeserializeObject<T>(v);

                if (analyzeResult != null)
                {
                    if (forceRefreshAction == null || !forceRefreshAction(argument, analyzeResult))
                    {
                        return new CacheKeyValue<T>
                        {
                            Key = key,
                            Value = analyzeResult,
                            CacheString = v,
                            ForceRefresh = false,
                            ValueSourceType = ValueSource.Cache.ToInt()
                        };
                    }
                }
            }
            catch (JsonSerializationException e)
            {
                e.Data.Add("json", v);

                throw;
            }
            catch (Exception e)
            {
                e.Data.Add("json", v);
            }
        }

        var value = valueBuildAction(argument);

        var expireTime = expireTimeBuildAction(argument);

        var json = JsonConvert.SerializeObject(value);

        Set(
            key,
            json,
            expireTime
        );

        return new CacheKeyValue<T>
        {
            Key = key,
            Value = value,
            SerializationSource = json,
            CacheString = json,
            ForceRefresh = true,
            ValueSourceType = ValueSource.Source.ToInt()
        };
    }

    #endregion

    #region TryGet<T, TArgument1, TArgument2>

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            (_, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (_, _, _) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new()
    {
        var resultDetectParameter = detectParameterAction(argument1, argument2);

        if (!resultDetectParameter.Success)
        {
            throw new UnhandledException(resultDetectParameter.Message);
        }

        if (keyBuildAction == null)
        {
            throw new UnhandledException("键名构建方法无效");
        }

        if (valueBuildAction == null)
        {
            throw new UnhandledException("键值构建方法无效");
        }

        var key = keyBuildAction(argument1, argument2);

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new UnhandledException("构建缓存键名时，得到了空白值");
        }

        var resultGetSerializedValue = GetSerializedValue(key);

        if (!resultGetSerializedValue.Success)
        {
            throw new UnhandledException(resultGetSerializedValue.Message);
        }

        var v = resultGetSerializedValue.Data ?? "";

        if (!string.IsNullOrWhiteSpace(v))
        {
            try
            {
                var analyzeResult = JsonConvert.DeserializeObject<T>(v);

                if (analyzeResult != null)
                {
                    if (forceRefreshAction == null || !forceRefreshAction(
                            argument1,
                            argument2,
                            analyzeResult
                        ))
                    {
                        return new CacheKeyValue<T>
                        {
                            Key = key,
                            Value = analyzeResult,
                            CacheString = v,
                            ForceRefresh = false,
                            ValueSourceType = ValueSource.Cache.ToInt()
                        };
                    }
                }
            }
            catch (JsonSerializationException e)
            {
                e.Data.Add("json", v);

                throw;
            }
            catch (Exception e)
            {
                e.Data.Add("json", v);
            }
        }

        var value = valueBuildAction(argument1, argument2);

        var expireTime = expireTimeBuildAction(argument1, argument2);

        var json = JsonConvert.SerializeObject(value);

        Set(
            key,
            json,
            expireTime
        );

        return new CacheKeyValue<T>
        {
            Key = key,
            Value = value,
            SerializationSource = json,
            CacheString = json,
            ForceRefresh = true,
            ValueSourceType = ValueSource.Source.ToInt()
        };
    }

    #endregion

    #region TryGet<T, TArgument1, TArgument2, TArgument3>

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            (_, _, _) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        Func<TArgument1, TArgument2, TArgument3, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, TArgument3, T, bool>? forceRefreshAction
    ) where T : new()
    {
        var resultDetectParameter = detectParameterAction(
            argument1,
            argument2,
            argument3
        );

        if (!resultDetectParameter.Success)
        {
            throw new UnhandledException(resultDetectParameter.Message);
        }

        if (keyBuildAction == null)
        {
            throw new UnhandledException("键名构建方法无效");
        }

        if (valueBuildAction == null)
        {
            throw new UnhandledException("键值构建方法无效");
        }

        var key = keyBuildAction(
            argument1,
            argument2,
            argument3
        );

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new UnhandledException("构建缓存键名时，得到了空白值");
        }

        var resultGetSerializedValue = GetSerializedValue(key);

        if (!resultGetSerializedValue.Success)
        {
            throw new UnhandledException(resultGetSerializedValue.Message);
        }

        var v = resultGetSerializedValue.Data ?? "";

        if (!string.IsNullOrWhiteSpace(v))
        {
            try
            {
                var analyzeResult = JsonConvert.DeserializeObject<T>(v);

                if (analyzeResult != null)
                {
                    if (forceRefreshAction == null || !forceRefreshAction(
                            argument1,
                            argument2,
                            argument3,
                            analyzeResult
                        ))
                    {
                        return new CacheKeyValue<T>
                        {
                            Key = key,
                            Value = analyzeResult,
                            CacheString = v,
                            ForceRefresh = false,
                            ValueSourceType = ValueSource.Cache.ToInt()
                        };
                    }
                }
            }
            catch (JsonSerializationException e)
            {
                e.Data.Add("json", v);

                throw;
            }
            catch (Exception e)
            {
                e.Data.Add("json", v);
            }
        }

        var value = valueBuildAction(
            argument1,
            argument2,
            argument3
        );

        var expireTime = expireTimeBuildAction(
            argument1,
            argument2,
            argument3
        );

        var json = JsonConvert.SerializeObject(value);

        Set(
            key,
            json,
            expireTime
        );

        return new CacheKeyValue<T>
        {
            Key = key,
            Value = value,
            SerializationSource = json,
            CacheString = json,
            ForceRefresh = true,
            ValueSourceType = ValueSource.Source.ToInt()
        };
    }

    #endregion

    #region TryGet<T, TArgument1, TArgument2, TArgument3, TArgument4>

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            (
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new()
    {
        var resultDetectParameter = detectParameterAction(
            argument1,
            argument2,
            argument3,
            argument4
        );

        if (!resultDetectParameter.Success)
        {
            throw new UnhandledException(resultDetectParameter.Message);
        }

        if (keyBuildAction == null)
        {
            throw new UnhandledException("键名构建方法无效");
        }

        if (valueBuildAction == null)
        {
            throw new UnhandledException("键值构建方法无效");
        }

        var key = keyBuildAction(
            argument1,
            argument2,
            argument3,
            argument4
        );

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new UnhandledException("构建缓存键名时，得到了空白值");
        }

        var resultGetSerializedValue = GetSerializedValue(key);

        if (!resultGetSerializedValue.Success)
        {
            throw new UnhandledException(resultGetSerializedValue.Message);
        }

        var v = resultGetSerializedValue.Data ?? "";

        if (!string.IsNullOrWhiteSpace(v))
        {
            try
            {
                var analyzeResult = JsonConvert.DeserializeObject<T>(v);

                if (analyzeResult != null)
                {
                    if (forceRefreshAction == null || !forceRefreshAction(
                            argument1,
                            argument2,
                            argument3,
                            argument4,
                            analyzeResult
                        ))
                    {
                        return new CacheKeyValue<T>
                        {
                            Key = key,
                            Value = analyzeResult,
                            CacheString = v,
                            ForceRefresh = false,
                            ValueSourceType = ValueSource.Cache.ToInt()
                        };
                    }
                }
            }
            catch (JsonSerializationException e)
            {
                e.Data.Add("json", v);

                throw;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        var value = valueBuildAction(
            argument1,
            argument2,
            argument3,
            argument4
        );

        var expireTime = expireTimeBuildAction(
            argument1,
            argument2,
            argument3,
            argument4
        );

        var json = JsonConvert.SerializeObject(value);

        Set(
            key,
            json,
            expireTime
        );

        return new CacheKeyValue<T>
        {
            Key = key,
            Value = value,
            SerializationSource = json,
            CacheString = json,
            ForceRefresh = true,
            ValueSourceType = ValueSource.Source.ToInt()
        };
    }

    #endregion

    #region TryGet<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            (
                _,
                _,
                _,
                _,
                _
            ) => new ExecutiveResult(ReturnCode.Ok),
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            forceRefreshAction
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new()
    {
        return TryGet(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5,
            detectParameterAction,
            keyBuildAction,
            valueBuildAction,
            expireTimeBuildAction,
            (
                _,
                _,
                _,
                _,
                _,
                _
            ) => forceRefresh
        );
    }

    public CacheKeyValue<T> TryGet<
        T,
        TArgument1,
        TArgument2,
        TArgument3,
        TArgument4,
        TArgument5
    >(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        TArgument5 argument5,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TimeSpan> expireTimeBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool>? forceRefreshAction
    ) where T : new()
    {
        var resultDetectParameter = detectParameterAction(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5
        );

        if (!resultDetectParameter.Success)
        {
            throw new UnhandledException(resultDetectParameter.Message);
        }

        if (keyBuildAction == null)
        {
            throw new UnhandledException("键名构建方法无效");
        }

        if (valueBuildAction == null)
        {
            throw new UnhandledException("键值构建方法无效");
        }

        var key = keyBuildAction(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5
        );

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new UnhandledException("构建缓存键名时，得到了空白值");
        }

        var resultGetSerializedValue = GetSerializedValue(key);

        if (!resultGetSerializedValue.Success)
        {
            throw new UnhandledException(resultGetSerializedValue.Message);
        }

        var v = resultGetSerializedValue.Data ?? "";

        if (!string.IsNullOrWhiteSpace(v))
        {
            try
            {
                var analyzeResult = JsonConvert.DeserializeObject<T>(v);

                if (analyzeResult != null)
                {
                    if (forceRefreshAction == null || !forceRefreshAction(
                            argument1,
                            argument2,
                            argument3,
                            argument4,
                            argument5,
                            analyzeResult
                        ))
                    {
                        return new CacheKeyValue<T>
                        {
                            Key = key,
                            Value = analyzeResult,
                            CacheString = v,
                            ForceRefresh = false,
                            ValueSourceType = ValueSource.Cache.ToInt()
                        };
                    }
                }
            }
            catch (JsonSerializationException e)
            {
                e.Data.Add("json", v);

                throw;
            }
            catch (Exception e)
            {
                e.Data.Add("json", v);
            }
        }

        var value = valueBuildAction(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5
        );

        var expireTime = expireTimeBuildAction(
            argument1,
            argument2,
            argument3,
            argument4,
            argument5
        );

        var json = JsonConvert.SerializeObject(value);

        Set(
            key,
            json,
            expireTime
        );

        return new CacheKeyValue<T>
        {
            Key = key,
            Value = value,
            SerializationSource = json,
            CacheString = json,
            ForceRefresh = true,
            ValueSourceType = ValueSource.Source.ToInt()
        };
    }

    #endregion

    #endregion
}