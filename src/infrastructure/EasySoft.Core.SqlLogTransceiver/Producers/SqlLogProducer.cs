using EasySoft.Core.Messaging.Abstractions;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.SqlLogTransceiver.Producers;

/// <inheritdoc />
public class SqlLogProducer : ISqlLogProducer
{
    private readonly IOutboundMessageBus _bus;

    private readonly IApplicationChannel _applicationChannel;

    /// <summary>
    /// SQL日志发布器
    /// </summary>
    /// <param name="bus">
    /// SQL日志的出站总线。通过 <see cref="FromKeyedServicesAttribute"/> 按 Key 注入，
    /// 不通过业务路由门面，避免污染业务路由表。
    /// </param>
    /// <param name="applicationChannel">应用通道</param>
    public SqlLogProducer(
        [FromKeyedServices(TransceiverConstant.SqlLogMessageServiceKey)]
        IOutboundMessageBus bus,
        IApplicationChannel applicationChannel
    )
    {
        _bus = bus;

        _applicationChannel = applicationChannel;
    }

    /// <inheritdoc />
    public Task SendAsync(
        string commandString,
        string executeType,
        string stackTraceSnippet,
        decimal startMilliseconds,
        decimal durationMilliseconds,
        decimal firstFetchDurationMilliseconds,
        int errored,
        int collectMode,
        string databaseChannel,
        CancellationToken cancellationToken = default
    )
    {
        var entity = new SqlLogMessage
        {
            CommandString = commandString,
            ExecuteType = executeType,
            StackTraceSnippet = stackTraceSnippet,
            StartMilliseconds = startMilliseconds,
            DurationMilliseconds = durationMilliseconds,
            FirstFetchDurationMilliseconds = firstFetchDurationMilliseconds,
            Errored = errored,
            CollectMode = collectMode,
            DatabaseChannel = databaseChannel,
            TriggerChannel = _applicationChannel.GetChannel().ToValue()
        };

        return SendAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public Task SendAsync(
        ISqlLogMessage sqlLogMessage,
        CancellationToken cancellationToken = default
    )
    {
        if (!SqlLogSwitchAssist.GetCurrentSwitch())
        {
            return Task.CompletedTask;
        }

        return _bus.Publish(sqlLogMessage, cancellationToken);
    }
}