using EasySoft.Core.LogServer.Core.Subscribers;

namespace EasySoft.Core.LogServer.Core.Extensions;

/// <summary>
/// WebApplicationBuilderExtensions
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// add permission server core logic
    /// </summary>
    /// <param name="builder"></param>    
    /// <returns></returns>
    internal static WebApplicationBuilder AddLogServerCore(
        this WebApplicationBuilder builder
    )
    {
        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddLogServerCore)}."
        );

        builder.AddCapSubscriber<ErrorLogExchangeSubscriber>();
        builder.AddCapSubscriber<GeneralLogExchangeSubscriber>();
        builder.AddCapSubscriber<SqlLogExchangeSubscriber>();

        return builder;
    }
}