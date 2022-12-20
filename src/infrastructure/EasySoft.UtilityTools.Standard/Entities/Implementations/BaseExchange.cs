using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.UtilityTools.Standard.Entities.Implementations;

/// <summary>
/// BaseExchange
/// </summary>
public abstract class BaseExchange : IExchangeEntity, IChannel, IIp, ICreate
{
    /// <summary>
    /// Id
    /// </summary>
    [Description("Id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <inheritdoc />
    public int Channel { get; set; }

    /// <inheritdoc />
    public string Ip { get; set; } = "";

    /// <inheritdoc />
    public long CreateBy { get; set; }

    /// <inheritdoc />
    public DateTime CreateTime { get; set; } = DateTimeOffset.Now.DateTime;

    /// <inheritdoc />
    public string GetId()
    {
        return Id;
    }

    /// <inheritdoc />
    public string GetIdentificationName()
    {
        return ReflectionAssist.GetPropertyName(() => Id);
    }
}