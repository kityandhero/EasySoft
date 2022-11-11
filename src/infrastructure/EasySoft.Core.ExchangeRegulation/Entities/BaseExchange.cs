using EasySoft.Core.ExchangeRegulation.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Entities;

public abstract class BaseExchange : IExchangeEntity
{
    public string Id { get; set; }

    /// <summary>
    /// 产生的来源标识（例如哪个产品的标识）
    /// </summary>
    public int Channel { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 生成Ip
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public long CreateUnixTime { get; set; }

    /// <summary>
    /// 创建人标识 
    /// </summary>
    public long CreateOperatorId { get; set; }

    protected BaseExchange()
    {
        Id = Guid.NewGuid().ToString();
        Ip = "";
        CreateUnixTime = DateTime.Now.ToUnixTime();
    }

    public string GetId()
    {
        return Id;
    }

    public string GetIdentificationName()
    {
        return ReflectionAssist.GetPropertyName(() => Id);
    }
}