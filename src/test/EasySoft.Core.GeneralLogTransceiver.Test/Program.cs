using EasySoft.Core.GeneralLogTransceiver.Extensions;
using EasySoft.Core.GeneralLogTransceiver.Producers;
using EasySoft.Core.GeneralLogTransceiver.Test;
using EasySoft.Core.GeneralLogTransceiver.Test.Consumers;
using EasySoft.Core.Messaging.SlimMessageBus.Extensions;
using EasySoft.UtilityTools.Core.Channels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApplicationChannel>(
    new ApplicationChannel().SetChannel(ChannelCollection.GeneralLogTransceiverTestApplication)
);

#region RabbitMQ Build

// * 拦截器 + 观测器 + 公共门面。不含任何子总线配置。
builder.Services.AddKernelMessaging();

// * 各模块独立注册自己的子总线，互不感知。
builder.Services.AddGeneralLogRabbitMQCore(builder.Configuration);

// 发消息
builder.Services.AddGeneralLogRabbitMQProducer(builder.Configuration);

// 收消息
builder.Services.AddGeneralLogRabbitMQConsumer<GeneralLogConsumer>(builder.Configuration);

// * 序列化配置为共享设置，任一次 AddSlimMessageBus 中声明一次即可。
builder.Services.AddSlimMessageBus(messageBusBuilder => messageBusBuilder.AddJsonSerializer());

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