using MediatR;

namespace EasySoft.Simple.MediatR.Console;

public class PingNotificationHandlerOne : INotificationHandler<PingNotification>
{
    public Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        System.Console.WriteLine("ping one");

        return Task.CompletedTask;
    }
}