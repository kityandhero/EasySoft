﻿namespace EasySoft.Core.EventBus.Cap.Serialization;

public class DefaultBinaryFormatterSerializer : ISerializer
{
    public Message Deserialize(string json)
    {
        throw new NotImplementedException();
    }

    public object Deserialize(object value, Type valueType)
    {
        throw new NotImplementedException();
    }

    public Task<Message> DeserializeAsync(TransportMessage transportMessage, Type? valueType)
    {
        throw new NotImplementedException();
    }

    public bool IsJsonType(object jsonObject)
    {
        throw new NotImplementedException();
    }

    public string Serialize(Message message)
    {
        throw new NotImplementedException();
    }

    public Task<TransportMessage> SerializeAsync(Message message)
    {
        throw new NotImplementedException();
    }
}