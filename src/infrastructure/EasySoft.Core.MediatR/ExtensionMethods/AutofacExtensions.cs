using System.Reflection;
using Autofac;
using MediatR;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class AutofacExtensions
{
    public static ContainerBuilder AddAdvanceMediator(this ContainerBuilder builder)
    {
        RegisterCore(builder);

        return builder;
    }

    public static ContainerBuilder AddAdvanceMediator(this ContainerBuilder builder, params Assembly[] assemblies)
    {
        RegisterCore(builder);

        builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();

        return builder;
    }

    private static void RegisterCore(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

        containerBuilder.Register<ServiceFactory>(context =>
        {
            var c = context.Resolve<IComponentContext>();

            return t => c.Resolve(t);
        });
    }
}