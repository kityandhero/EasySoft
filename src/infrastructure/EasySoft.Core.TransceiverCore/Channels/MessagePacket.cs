namespace EasySoft.Core.TransceiverCore.Channels;

/// <summary>
/// 消息包封装结构
/// <para>提前序列化后的JSON文本包，转发、消费全程复用该字符串，避免重复序列化开销</para>
/// </summary>
public readonly struct MessagePacket
{
    /// <summary>
    /// 序列化完成后的JSON原始文本
    /// </summary>
    public string JsonText { get; }

    /// <param name="json">
    /// 序列化JSON文本
    /// </param>
    public MessagePacket(string json)
    {
        JsonText = json;
    }
}