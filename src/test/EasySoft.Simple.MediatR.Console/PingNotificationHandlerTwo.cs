using MediatR;

namespace EasySoft.Simple.MediatR.Console;

public class PingNotificationHandlerTwo : INotificationHandler<PingNotification>
{
    public Task Handle(PingNotification notification, CancellationToken cancellationToken)
    {
        System.Console.WriteLine("ping two");

        return Task.CompletedTask;
    }
}