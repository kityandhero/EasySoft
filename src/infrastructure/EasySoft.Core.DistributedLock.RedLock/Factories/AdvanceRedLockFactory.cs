using EasySoft.Core.DistributedLock.RedLock.Interfaces;
using Microsoft.Extensions.Logging;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;

namespace EasySoft.Core.DistributedLock.RedLock.Factories;

public sealed class AdvanceRedLockFactory : RedLockFactory, IAdvanceRedLockFactory
{
    public AdvanceRedLockFactory(RedLockConfiguration configuration) : base(configuration)
    {
    }

    public static AdvanceRedLockFactory CreateFactory(
        IList<RedLockEndPoint> endPoints,
        ILoggerFactory? loggerFactory = null
    )
    {
        return (AdvanceRedLockFactory)Create(endPoints, loggerFactory);
    }
}