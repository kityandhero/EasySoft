namespace EasySoft.Core.EventBus.Cap.Serialization;

/// <summary>
/// 空序列化器实现
/// 用于单元测试/模拟环境，所有方法直接抛异常，不做实际序列化
/// 补齐ISerializer全部接口成员，包含老版string同步序列化方法
/// </summary>
public class DefaultBinaryFormatterSerializer : ISerializer
{
    /// <summary>
    /// 将消息对象序列化为字符串（老接口保留，CAP内部兼容使用）
    /// </summary>
    /// <param name="message">CAP消息实体</param>
    /// <returns>序列化后的json字符串</returns>
    public string Serialize(Message message)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 将字符串反序列化为Message
    /// </summary>
    /// <param name="json">序列化字符串</param>
    /// <returns>CAP消息实体，允许null</returns>
    public Message? Deserialize(string json)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 异步将消息对象序列化为TransportMessage
    /// </summary>
    /// <param name="message">CAP消息实体</param>
    /// <returns>传输消息载体</returns>
    public ValueTask<TransportMessage> SerializeAsync(Message message)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 将TransportMessage反序列化为Message，同时指定消息体目标类型
    /// </summary>
    /// <param name="transportMessage">底层传输消息</param>
    /// <param name="valueType">消息内容的实体类型，可为null</param>
    /// <returns>CAP消息实体</returns>
    public ValueTask<Message> DeserializeAsync(TransportMessage transportMessage, Type? valueType)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 判断对象是否为Json类型对象
    /// </summary>
    /// <param name="jsonObject">待判断对象</param>
    /// <returns>固定false，空实现</returns>
    public bool IsJsonType(object jsonObject)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 反序列化对象到指定类型
    /// </summary>
    /// <param name="value">待反序列化原始值</param>
    /// <param name="valueType">目标类型</param>
    /// <returns>反序列化后的实例</returns>
    public object Deserialize(object value, Type valueType)
    {
        throw new NotImplementedException();
    }
}