using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.DistributedLock.RedLock.Factories;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.DistributedLock.RedLock.Assist;

internal static class RedLockAssist
{
    private static AdvanceRedLockFactory? _lockFactory;

    internal static AdvanceRedLockFactory GetAdvanceRedLockFactory(RedLockOptions options)
    {
        if (_lockFactory != null) return _lockFactory;

        if (!options.RedisAddressList.ToListFilterNullOrWhiteSpace().Any())
            throw new ArgumentException("RedLockOptions config error,redisAddressList need more than one redis master");

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