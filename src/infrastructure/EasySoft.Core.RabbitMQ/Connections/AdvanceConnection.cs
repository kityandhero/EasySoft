namespace EasySoft.Core.RabbitMQ.Connections;

public class AdvanceConnection : IAdvanceConnection, IHostedService
{
    private readonly IConnectionFactory _factory;
    private readonly ILogger<AdvanceConnection> _logger;

    public IConnection Connection { get; private set; }

    public AdvanceConnection(
        IConnectionFactory factory,
        ILogger<AdvanceConnection> logger
    )
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var retryPolicy = Policy
            .Handle<SocketException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetryAsync(
                2,
                _ => TimeSpan.FromSeconds(1),
                (
                    ex,
                    timespan,
                    retryCount,
                    _
                ) =>
                {
                    _logger.LogError(
                        ex,
                        "RabbitMQ连接失败，准备第{RetryCount}次重试，等待{Delay}s | {ErrorMessage}",
                        retryCount,
                        timespan.TotalSeconds,
                        ex.Message
                    );

                    return Task.CompletedTask;
                }
            );

        Connection = await retryPolicy.ExecuteAsync(
            async ct => await _factory.CreateConnectionAsync(ct),
            cancellationToken
        );

        _logger.LogInformation("RabbitMQ主连接创建成功");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (Connection != null && Connection.IsOpen)
        {
            await Connection.CloseAsync(cancellationToken);

            await Connection.DisposeAsync();

            _logger.LogInformation("RabbitMQ主连接已关闭");
        }
    }
}