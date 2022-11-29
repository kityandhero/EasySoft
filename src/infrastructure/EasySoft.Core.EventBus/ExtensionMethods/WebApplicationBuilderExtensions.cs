namespace EasySoft.Core.EventBus.ExtensionMethods;

public static class WebApplicationBuilderExtensions
{
    private const string UniqueIdentifierAddCapEventBus = "b487b98c-355d-46f8-be1b-05b9032422d7";

    /// <summary>
    /// 注册CAP事件订阅
    /// </summary>
    public static WebApplicationBuilder AddCapEventSubscriber<TSubscriber>(
        this WebApplicationBuilder builder
    ) where TSubscriber : class, ICapSubscribe
    {
        if (builder.HasRegistered(UniqueIdentifierAddCapEventBus))
            return builder;

        StartupDescriptionMessageAssist.AddExecute(
            $"{nameof(AddCapEventSubscriber)}<{typeof(TSubscriber).Name}>."
        );

        builder.Services.AddCapEventSubscriber<TSubscriber>();

        return builder;
    }
}