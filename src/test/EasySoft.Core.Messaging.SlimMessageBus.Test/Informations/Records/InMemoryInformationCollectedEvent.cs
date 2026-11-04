namespace EasySoft.Core.Messaging.SlimMessageBus.Test.Informations.Records;

public sealed record InMemoryInformationCollectedEvent(
    string Source,
    string Category,
    string Payload,
    DateTimeOffset Timestamp
);