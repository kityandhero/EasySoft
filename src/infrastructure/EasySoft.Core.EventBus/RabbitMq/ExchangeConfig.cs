﻿namespace EasySoft.Core.EventBus.RabbitMq;

public class ExchangeConfig
{
    public string Name { get; set; } = string.Empty;

    public ExchangeType Type { get; set; } = default!;

    public string DeadExchangeName { get; set; } = string.Empty;
}