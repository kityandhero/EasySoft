using EasySoft.Core.AdvanceNLog.ExtensionMethods;
using EasySoft.Core.GeneralLogTransceiver.Collectors;
using EasySoft.Core.GeneralLogTransceiver.Extensions;
using EasySoft.Core.GeneralLogTransceiver.Producers;
using EasySoft.Core.GeneralLogTransceiver.Test;
using EasySoft.Core.GeneralLogTransceiver.Test.Consumers;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Producers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApplicationChannel>(
    new ApplicationChannel().SetChannel(ChannelCollection.GeneralLogTransceiverTestApplication)
);

EnvironmentAssist.SetEnvironment(builder.Environment);

builder.AddAdvanceNLog();

#region RabbitMQ Build

builder.Services.AddKernelMessagingWithJson();

builder.AddGeneralLogRabbitMQOnlyProducer();

#endregion

var app = builder.Build();

#region RabbitMQ Test Producer

var generalLogCollector = app.Services.GetRequiredService<GeneralLogCollector>();

app.Lifetime.ApplicationStarted.Register(() =>
    {
        _ = Task.Run(async () =>
            {
                await Task.Delay(5000);

                for (var i = 0; i < 100; i++)
                {
                    generalLogCollector.Publish($"Test-{i}");
                }

                app.Logger.LogAdvanceInfo("[Test] 100条已投递，等待3秒让消费落盘...");

                await Task.Delay(3000); // 关键：等消费者跑完

                app.Logger.LogAdvanceInfo("[Test] 等待结束");
            }
        );
    }
);

#endregion

app.Run();