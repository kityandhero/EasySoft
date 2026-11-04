using EasySoft.Core.RabbitMQ.Exchanges;

namespace EasySoft.Core.RabbitMQ.ExtensionMethods;

/// <summary>
/// ExchangeConfig 扩展方法
/// </summary>
public static class ExchangeConfigExtension
{
    /// <summary>
    /// 获取装配好的名称
    /// </summary>
    /// <param name="exchangeConfig">原始配置</param> 
    /// <returns>装配好的名称</returns>
    public static string GetAssembledName(
        this ExchangeConfig exchangeConfig
    )
    {
        if (string.IsNullOrWhiteSpace(exchangeConfig.GlobalPrefix))
        {
            return exchangeConfig.Name;
        }

        return $"{exchangeConfig.GlobalPrefix}.{exchangeConfig.Name}";
    }
}