using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// HostApplicationBuilderExtensions
/// </summary>
public static class HostApplicationBuilderExtensions
{
    private static readonly ConcurrentDictionary<string, char> RegisteredModels = new();

    private const string IdentifierAddApplicationCore = "2fd0664caeb34924b1b72a5bf374b3be";

    private const string IdentifierAddAdvanceApplicationChannel = "ea712530180e4c6580172ef9f9f4a794";

    /// <summary>
    /// HasRegistered
    /// </summary>
    /// <param name="_">构建器</param>
    /// <param name="modelName">模组名</param>
    /// <returns></returns>
    public static bool HasRegistered(this IHostApplicationBuilder _, string modelName)
    {
        return !RegisteredModels.TryAdd(modelName.ToLower(), '1');
    }

    /// <summary>
    /// 注入应用核
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <returns>构建器</returns>
    public static IHostApplicationBuilder AddApplicationCore(
        this IHostApplicationBuilder builder
    )
    {
        if (builder.HasRegistered(IdentifierAddApplicationCore))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute($"{nameof(AddApplicationCore)}");

        builder.Services.AddApplicationConfigureAssist(builder.Configuration);

        builder.Services.AddJsonSerializerSettings();

        return builder;
    }

    /// <summary>
    /// 标记当前应用通道值, 用于远程日志等数据中, 便于数据辨认, 不使用此方法标记, 框架将采用内置值 0 代替
    /// </summary>
    /// <param name="builder">构建器</param>
    /// <param name="channel">应用通道</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IHostApplicationBuilder AddAdvanceApplicationChannel(
        this IHostApplicationBuilder builder,
        IChannel channel
    )
    {
        if (builder.HasRegistered(IdentifierAddAdvanceApplicationChannel))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceApplicationChannel)}."
        );

        builder.Services.AddAdvanceApplicationChannel(channel);

        return builder;
    }

    private static IHostApplicationBuilder AddAdvanceApplicationChannel<T>(
        this IHostApplicationBuilder builder,
        T channel
    ) where T : IChannel
    {
        if (builder.HasRegistered(IdentifierAddAdvanceApplicationChannel))
        {
            return builder;
        }

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddAdvanceApplicationChannel)}<{typeof(T).Name}>."
        );

        builder.Services.AddAdvanceApplicationChannel(channel);

        return builder;
    }
}