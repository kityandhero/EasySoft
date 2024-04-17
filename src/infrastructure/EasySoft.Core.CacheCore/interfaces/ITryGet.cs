using EasySoft.Core.CacheCore.Models;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Core.CacheCore.interfaces;

public interface ITryGet
{
    #region TryGetWithSuperiorStorage

    #region TryGetWithSuperiorStorage<T, TArgument>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 20 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 20 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
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
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 20 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSuperiorStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2, TArgument3>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGetWithSuperiorStorage<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>  
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新, 缓存时间胃 20 ~ 40 秒
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>  
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #endregion

    #region TryGetWithProvisionalStorage

    #region TryGetWithProvisionalStorage<T, TArgument>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
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
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithProvisionalStorage<T, TArgument1, TArgument2>(
        TArgument1 argument1,
        TArgument2 argument2,
        Func<TArgument1, TArgument2, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, string> keyBuildAction,
        Func<TArgument1, TArgument2, T> valueBuildAction,
        Func<TArgument1, TArgument2, T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2, TArgument3>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGetWithProvisionalStorage<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（限制为凌晨的2点~5:59点之间）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #endregion

    #region TryGetWithSteadyStorage

    #region TryGetWithSteadyStorage<T, TArgument>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4>(
        TArgument1 argument1,
        TArgument2 argument2,
        TArgument3 argument3,
        TArgument4 argument4,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, ExecutiveResult> detectParameterAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, string> keyBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T> valueBuildAction,
        Func<TArgument1, TArgument2, TArgument3, TArgument4, T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGetWithSteadyStorage<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新（60 ~ 120 天）
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #endregion

    #region TryGet

    #region TryGet<T>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T>(
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T>(
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        Func<T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T>(
        Func<ExecutiveResult> detectParameterAction,
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T>(
        Func<ExecutiveResult> detectParameterAction,
        Func<string> keyBuildAction,
        Func<T> valueBuildAction,
        Func<TimeSpan> expireTimeBuildAction,
        Func<T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGet<T, TArgument>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>  
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        bool forceRefresh = false
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument">参数类型</typeparam>  
    /// <returns>返回指定类型值</returns>
    public CacheKeyValue<T> TryGet<T, TArgument>(
        TArgument argument,
        Func<TArgument, ExecutiveResult> detectParameterAction,
        Func<TArgument, string> keyBuildAction,
        Func<TArgument, T> valueBuildAction,
        Func<TArgument, TimeSpan> expireTimeBuildAction,
        Func<TArgument, T, bool>? forceRefreshAction
    ) where T : new();

    #endregion

    #region TryGet<T, TArgument1, TArgument2>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGet<T, TArgument1, TArgument2, TArgument3>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGet<T, TArgument1, TArgument2, TArgument3, TArgument4>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    #endregion

    #region TryGet<T, TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefresh">强制更新</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
    ) where T : new();

    /// <summary>
    /// 从缓存获取值，获取失败时通过定义的动作进行更新
    /// </summary>
    /// <param name="argument1">参数</param>
    /// <param name="argument2">参数</param>
    /// <param name="argument3">参数</param>
    /// <param name="argument4">参数</param>
    /// <param name="argument5">参数</param>
    /// <param name="detectParameterAction">参数预检测，未通过检测人将触发异常</param>
    /// <param name="keyBuildAction">缓存键名构建逻辑</param>
    /// <param name="valueBuildAction">缓存值设置逻辑</param>
    /// <param name="expireTimeBuildAction">过期时间构建逻辑</param>
    /// <param name="forceRefreshAction">强制更新逻辑</param>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <typeparam name="TArgument1">参数类型</typeparam>
    /// <typeparam name="TArgument2">参数类型</typeparam>
    /// <typeparam name="TArgument3">参数类型</typeparam>
    /// <typeparam name="TArgument4">参数类型</typeparam>
    /// <typeparam name="TArgument5">参数类型</typeparam>
    /// <returns>返回指定类型值</returns>
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
        Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, T, bool> forceRefreshAction
    ) where T : new();

    #endregion

    #endregion
}