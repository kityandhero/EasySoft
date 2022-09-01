using Autofac;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Startup;
using EasySoft.Core.Mapster.Assists;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.Mapster.ExtensionMethods;

// 映射配置应该只初始化并且只进行一次配置。因此在编写代码的时候不能将映射配置和映射调用放在同一个地方。
// 重复进行相同配饰实例化将引发异常
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// AddAdvanceMapster, 配置构建请向 ConfigActionAssist 增加配置 action
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddAdvanceMapster(
        this WebApplicationBuilder builder
    )
    {
        ConfigActionAssist.GetConfigAction().ForEach(action => { action(TypeAdapterConfig.GlobalSettings); });

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<Mapper>().As<IMapper>().SingleInstance();
        });

        StartupDescriptionMessageAssist.Add(
            new StartupMessage()
                .SetMessage(
                    "IMapper provide by Mapster inject complete, you can config it with ConfigActionAssist, the easy way to use is MapperAssist."
                )
        );

        return builder;
    }
}