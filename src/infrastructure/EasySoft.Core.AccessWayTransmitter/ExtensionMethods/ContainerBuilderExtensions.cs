using Autofac;
using EasySoft.Core.AccessWayTransmitter.Producers;

namespace EasySoft.Core.AccessWayTransmitter.ExtensionMethods;

public static class ContainerBuilderExtensions
{
    public static ContainerBuilder AddAccessWayTransmitter(
        this ContainerBuilder builder
    )
    {
        builder.RegisterType<AccessWayProducer>().As<IAccessWayProducer>().SingleInstance();

        return builder;
    }
}