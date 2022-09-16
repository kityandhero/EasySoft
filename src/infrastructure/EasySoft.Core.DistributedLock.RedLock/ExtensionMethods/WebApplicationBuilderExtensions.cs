using EasySoft.Core.DistributedLock.RedLock.Assist;
using EasySoft.Core.DistributedLock.RedLock.Configure;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.DistributedLock.RedLock.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAdvanceRedLock(
        this WebApplicationBuilder builder,
        Action<RedLockOptions> action
    )
    {
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