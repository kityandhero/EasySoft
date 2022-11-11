using EasySoft.Core.DistributedLock.RedLock.Assist;
using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.DistributedLock.RedLock.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// RedLock 简介
    /// RedLock 的思想是使用多台 Redis Master ，节点完全独立，节点间不需要进行数据同步，
    /// 因为 Master-Slave 架构一旦 Master 发生故障时数据没有复制到 Slave，被选为 Master 的 Slave 就丢掉了锁，另一个客户端就可以再次拿到锁。锁通过 setNX（原子操作） 命令设置，
    /// 在有效时间内当获得锁的数量大于 (n/2+1) 代表成功，失败后需要向所有节点发送释放锁的消息。
    /// 该方法较为消耗资源，并要确保部署方式。
    /// </summary>
    public static WebApplicationBuilder AddAdvanceRedLock(
        this WebApplicationBuilder builder,
        Action<RedLockOptions> action
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceRedLock)}."
        );

        var options = new RedLockOptions();

        action(options);

        builder.Host.AddAdvanceRedLock(options);

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>().SetName("UseAdvanceRedLock").SetAction(application =>
            {
                application.Lifetime.ApplicationStopping.Register(RedLockAssist.DisposeFactory);
            })
        );

        return builder;
    }
}