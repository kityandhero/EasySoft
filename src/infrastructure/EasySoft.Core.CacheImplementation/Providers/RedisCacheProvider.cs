using EasySoft.Core.DependencyInjection.IocAssists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.CacheImplementation.Providers;

public static class RedisCacheProvider
{
    private static readonly Lock Locker = new();

    private static ConnectionMultiplexer _instance;

    public static IDatabase GetDatabase(string connection)
    {
        return GetConnectionMultiplexer(connection).GetDatabase();
    }

    /// <summary>
    /// 单例获取
    /// </summary>
    private static ConnectionMultiplexer GetConnectionMultiplexer(string connection)
    {
        if (_instance != null)
        {
            return _instance;
        }

        lock (Locker)
        {
            if (!(_instance is { IsConnected: true }))
            {
                _instance = GetManager(connection);
            }
        }

        return _instance;
    }

    private static ConnectionMultiplexer GetManager(string connection)
    {
        if (string.IsNullOrWhiteSpace(connection))
        {
            throw new UnhandledException("缺少 Redis 连接配置");
        }

        try
        {
            var redis = ConnectionMultiplexer.Connect(connection);

            redis.GetDatabase();

            //注册如下事件
            redis.ConnectionFailed += MuxerConnectionFailed;
            redis.ConnectionRestored += MuxerConnectionRestored;
            redis.ErrorMessage += MuxerErrorMessage;
            redis.ConfigurationChanged += MuxerConfigurationChanged;
            redis.HashSlotMoved += MuxerHashSlotMoved;
            redis.InternalError += MuxerInternalError;

            return redis;
        }
        catch (Exception ex)
        {
            LogError(ex);

            if (ex.InnerException != null)
            {
                LogError(ex);
            }

            throw new UnhandledException(
                $"连接Redis失败，连接字符串为{connection}；内部消息：{ex.Message}"
            );
        }
    }

    #region 事件

    /// <summary>
    /// 配置更改时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
    {
        LogError("Configuration changed: " + e.EndPoint);
    }

    /// <summary>
    /// 发生错误时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
    {
        LogError("ErrorMessage: " + e.Message);
    }

    /// <summary>
    /// 重新建立连接之前的错误
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
    {
        LogError("ConnectionRestored: " + e.EndPoint);
    }

    /// <summary>
    /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
    {
        LogError(
            "重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType +
            (e.Exception == null ? "" : ", " + e.Exception.Message)
        );
    }

    /// <summary>
    /// 更改集群
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
    {
        LogError("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
    }

    /// <summary>
    /// redis类库错误
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
    {
        LogError(e.Exception);
    }

    #endregion 事件

    #region LogError

    /// <summary>
    /// 记录执行的sql
    /// </summary>
    /// <param name="ex"></param>
    private static void LogError(Exception ex)
    {
        var errorLogProducer = ServiceProviderAssist.Instance.Resolve<IErrorLogProducer>();

        if (errorLogProducer == null)
        {
            return;
        }

        var channel = ServiceProviderAssist.Instance.Resolve<IChannel>();

        if (channel == null)
        {
            return;
        }

        var log = ErrorLogMessageAssist.CreateApplicationErrorLog(channel);

        log.Fill(ex);

        errorLogProducer.SendAsync(log);
    }

    /// <summary>
    /// 记录执行的sql
    /// </summary>
    /// <param name="message"></param>
    private static void LogError(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        var errorLogProducer = ServiceProviderAssist.Instance.Resolve<IErrorLogProducer>();

        if (errorLogProducer == null)
        {
            return;
        }

        var channel = ServiceProviderAssist.Instance.Resolve<IChannel>();

        if (channel == null)
        {
            return;
        }

        var log = ErrorLogMessageAssist.CreateApplicationErrorLog(channel);

        log.SetMessage(message);

        errorLogProducer.SendAsync(log);
    }

    #endregion

    /// <summary>
    /// edis的keys模糊查询：
    /// local ks为定义一个局部变量，其中用于存储获取到的keys
    /// #ks为ks集合的个数, 语句的意思： for(int i = 1; i 《= ks.Count; i+=5000)
    /// Lua集合索引值从1为起始, unpack为解包，获取ks集合中的数据，每次5000，然后执行删除
    /// </summary>
    internal static readonly LuaScript RemoveScript = LuaScript.Prepare(
        " local ks = redis.call('KEYS', @keyPattern) " +
        " for i=1,#ks,5000 do " +
        "     redis.call('del', unpack(ks, i, math.min(i+4999, #ks))) " +
        " end " +
        " return true "
    );
}