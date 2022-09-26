// See https://aka.ms/new-console-template for more information

using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.MediatR.ExtensionMethods;
using EasySoft.Simple.MediatR.Console;
using MediatR;

// https://github.com/jbogard/MediatR/wiki

var serviceProvider = AutoFacConsoleAssist.CreateServiceProvider(
    services => { },
    containerBuilder => { containerBuilder.AddAdvanceMediator(typeof(Ping).Assembly); }
);

var mediator = AutofacAssist.Instance.Resolve<IMediator>();

var response = await mediator.Send(new Ping());

Console.WriteLine(response);

await mediator.Publish(new PingNotification());