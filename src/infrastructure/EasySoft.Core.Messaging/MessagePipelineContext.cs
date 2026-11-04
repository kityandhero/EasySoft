using EasySoft.Core.Messaging.Abstractions;

namespace EasySoft.Core.Messaging;

/// <summary>
/// 管线上下文，交给 <see cref="IMessagePipelineObserver"/> 生成作用域。
/// </summary>
/// <param name="Operation">当前阶段。</param>
/// <param name="UnitKey">所属隔离单元 Key。</param>
/// <param name="MessageType">消息运行时类型。</param>
/// <param name="Destination">目标路径（队列/交换机名，取决于传输层）。</param>
public readonly record struct MessagePipelineContext(
    MessageOperation Operation,
    string UnitKey,
    Type MessageType,
    string Destination
);