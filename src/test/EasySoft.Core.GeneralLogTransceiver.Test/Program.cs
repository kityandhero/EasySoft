using EasySoft.Core.GeneralLogTransceiver.Extensions;
using EasySoft.Core.GeneralLogTransceiver.Producers;
using EasySoft.Core.GeneralLogTransceiver.Test;
using EasySoft.Core.GeneralLogTransceiver.Test.Consumers;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.UtilityTools.Core.Channels;
using EasySoft.UtilityTools.Standard.Producers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApplicationChannel>(
    new ApplicationChannel().SetChannel(ChannelCollection.GeneralLogTransceiverTestApplication)
);

#region RabbitMQ Build

builder.Services.AddKernelMessagingWithJson();

builder.AddGeneralLogRabbitMQTransceiver<GeneralLogConsumer>();

#endregion

var app = builder.Build();

#region RabbitMQ Test Producer

// 宿主启动完成后触发，此时后台泵已就绪。
app.Lifetime.ApplicationStarted.Register(() =>
    {
        _ = Task.Run(async () =>
            {
                var generalLogProducer = app.Services.GetRequiredService<IGeneralLogProducer>();
                var sw = Stopwatch.StartNew();

                for (var i = 0; i < 100; i++)
                {
                    await generalLogProducer.SendAsync($"Test-{i}");
                }

                sw.Stop();

                Console.WriteLine($"Fired 100 sends in {sw.ElapsedMilliseconds}ms");
            }
        );
    }
);

#endregion

app.Run();