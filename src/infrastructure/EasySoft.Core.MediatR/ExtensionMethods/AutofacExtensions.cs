namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class AutofacExtensions
{
    public static ContainerBuilder AddAdvanceMediator(this ContainerBuilder builder, Assembly assembly)
    {
        RegisterCore(builder);

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

        return builder;
    }

    public static ContainerBuilder AddAdvanceMediator(this ContainerBuilder builder, IEnumerable<Assembly> assemblies)
    {
        RegisterCore(builder);

        builder.RegisterAssemblyTypes(assemblies.ToArray()).AsImplementedInterfaces();

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