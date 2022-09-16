using System.Net;
using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.DistributedLock.RedLock.Factories;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using RedLockNet.SERedis.Configuration;

namespace EasySoft.Core.DistributedLock.RedLock.Assist;

/// <summary>
/// RedLock 简介
/// RedLock 的思想是使用多台 Redis Master ，节点完全独立，节点间不需要进行数据同步，
/// 因为 Master-Slave 架构一旦 Master 发生故障时数据没有复制到 Slave，被选为 Master 的 Slave 就丢掉了锁，另一个客户端就可以再次拿到锁。锁通过 setNX（原子操作） 命令设置，
/// 在有效时间内当获得锁的数量大于 (n/2+1) 代表成功，失败后需要向所有节点发送释放锁的消息。
/// 该方法较为消耗资源，并休要确保部署方式。
/// </summary>
internal static class RedLockAssist
{
    private static AdvanceRedLockFactory? _lockFactory;

    internal static AdvanceRedLockFactory GetAdvanceRedLockFactory(RedLockOptions options)
    {
        if (_lockFactory != null)
        {
            return _lockFactory;
        }

        if (!options.RedisAddressList.ToListFilterNullOrWhiteSpace().Any())
        {
            throw new ArgumentException("RedLockOptions config error,redisAddressList need more than one redis master");
        }

        var endPoints = options.RedisAddressList.Select(item => item.Split(":"))
            .Select(address => new DnsEndPoint(address[0], Convert.ToInt32(address[1])))
            .Select(dummy => (RedLockEndPoint)dummy).ToList();

        _lockFactory = AdvanceRedLockFactory.CreateFactory(endPoints);

        return _lockFactory;
    }

    internal static void DisposeFactory()
    {
        _lockFactory?.Dispose();
    }
}