using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EasySoft.Core.MediatR.ExtensionMethods;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services, Assembly assembly)
    {
        return services.AddMediatR(typeof(Order).Assembly, assembly);
    }
}