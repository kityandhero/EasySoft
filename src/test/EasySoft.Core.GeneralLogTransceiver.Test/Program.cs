using EasySoft.Core.AccessWayTransceiver.Collectors;
using EasySoft.Core.AccessWayTransceiver.Extensions;
using EasySoft.Core.AdvanceNLog.AspNetCore.ExtensionMethods;
using EasySoft.Core.ChannelCheckTransceiver.Extensions;
using EasySoft.Core.ErrorLogTransceiver.Extensions;
using EasySoft.Core.GeneralLogTransceiver.Test;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApplicationChannel>(
    new ApplicationChannel().SetChannel(ChannelCollection.GeneralLogTransceiverTestApplication)
);

HostEnvironmentAssist.SetEnvironment(builder.Environment);

builder.AddAdvanceNLog();

#region RabbitMQ Build

builder.Services.AddKernelMessagingWithJson();

builder.AddAccessWayRabbitMQOnlyProducer();

// builder.AddAccessWayRabbitMQTransceiver<CustomAccessWayConsumer>();

// builder.AddGeneralLogRabbitMQOnlyProducer();

builder.AddErrorLogRabbitMQOnlyProducer();

// builder.AddSqlLogRabbitMQOnlyProducer();

builder.AddChannelCheckRabbitMQOnlyProducer();

#endregion

var app = builder.Build();

#region RabbitMQ Test Producer

var collector = app.Services.GetRequiredService<AccessWayCollector>();

app.Lifetime.ApplicationStarted.Register(() =>
    {
        _ = Task.Run(async () =>
            {
                await Task.Delay(5000);

                for (var i = 0; i < 100; i++)
                {
                    collector.Publish(
                        new AccessWayTransitiveMessage
                        {
                            // Message = $"test-error-{i}"

                            // Channel = $"test-channel-{i}"

                            GuidTag = $"test-access_way-{i}"
                        }
                    );
                }

                app.Logger.LogAdvanceInfo(
                    $"MainQueued: {collector.GetMetrics().MainQueued.ToString()}"
                );

                app.Logger.LogAdvanceInfo(
                    $"FileSink: {collector.GetMetrics().FileSink.CurrentQueued.ToString()}"
                );

                app.Logger.LogAdvanceInfo(
                    $"TransferSink: {collector.GetMetrics().TransferSink.CurrentQueued.ToString()}"
                );

                app.Logger.LogAdvanceInfo("[Test] 100条已投递，等待3秒让消费落盘...");

                await Task.Delay(3000); // 关键：等消费者跑完

                app.Logger.LogAdvanceInfo("[Test] 等待结束");
            }
        );
    }
);

#endregion

app.Run();