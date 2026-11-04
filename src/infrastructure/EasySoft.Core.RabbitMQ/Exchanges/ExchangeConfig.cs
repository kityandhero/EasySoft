namespace EasySoft.Core.RabbitMQ.Exchanges;

/// <summary>
/// 交换机配置
/// </summary>
public class ExchangeConfig
{
    public string Name { get; set; } = string.Empty;

    public ExchangeType Type { get; set; } = default!;

    /// <summary>
    /// 全局前缀，用于做同类系统不同部署的数据隔离
    /// </summary>
    public string GlobalPrefix { get; set; } = string.Empty;
}