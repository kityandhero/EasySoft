using System.Reflection;
using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace EasySoft.Core.AutoMapper.Modules;

internal class AutoMapperModule : Module
{
    private readonly Assembly[] assembliesToScan;
    private readonly Action<IMapperConfigurationExpression> mappingConfigurationAction;
    private readonly bool propertiesAutowired;

    public AutoMapperModule(Assembly[] assembliesToScan,
        Action<IMapperConfigurationExpression> mappingConfigurationAction,
        bool propertiesAutowired)
    {
        this.assembliesToScan = assembliesToScan ?? throw new ArgumentNullException(nameof(assembliesToScan));
        this.mappingConfigurationAction = mappingConfigurationAction ??
                                          throw new ArgumentNullException(nameof(mappingConfigurationAction));
        this.propertiesAutowired = propertiesAutowired;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var distinctAssemblies = this.assembliesToScan
            .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
            .Distinct()
            .ToArray();

        var profiles = builder.RegisterAssemblyTypes(distinctAssemblies)
            .AssignableTo(typeof(Profile))
            .As<Profile>()
            .SingleInstance();

        if (propertiesAutowired)
        {
            profiles.PropertiesAutowired();
        }

        builder.Register(componentContext =>
            {
                var mapperConfiguration = new MapperConfiguration(
                    config => ConfigurationAction(config, componentContext)
                );

                // throw new Exception("mapperConfiguration disallow null");

                Console.WriteLine("mapperConfiguration is null");

                return mapperConfiguration;
            })
            .AsSelf()
            .As<IConfigurationProvider>()
            .SingleInstance();

        var openTypes = new[]
        {
            typeof(IValueResolver<,,>),
            typeof(IValueConverter<,>),
            typeof(IMemberValueResolver<,,,>),
            typeof(ITypeConverter<,>),
            typeof(IMappingAction<,>)
        };

        foreach (var openType in openTypes)
        {
            var openTypeBuilder = builder.RegisterAssemblyTypes(distinctAssemblies)
                .AsClosedTypesOf(openType)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            if (propertiesAutowired)
                openTypeBuilder.PropertiesAutowired();
        }

        builder.Register(componentContext =>
                {
                    var mapperConfiguration = componentContext.Resolve<MapperConfiguration>();

                    // var   componentContext=     componentContext.Resolve<IComponentContext>();

                    var mapper = mapperConfiguration.CreateMapper(componentContext.Resolve);

                    return mapper;
                }
            )
            .As<IMapper>()
            .InstancePerLifetimeScope();
    }

    private void ConfigurationAction(IMapperConfigurationExpression cfg, IComponentContext componentContext)
    {
        this.mappingConfigurationAction.Invoke(cfg);

        var profiles = componentContext.Resolve<IEnumerable<Profile>>();

        foreach (var profile in profiles)
        {
            cfg.AddProfile(profile);
        }
    }
}