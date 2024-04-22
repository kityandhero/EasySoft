namespace EasySoft.Simple.Distribute.Common.Enums;

/// <summary>
/// ApplicationChannelCollection
/// </summary>
public abstract class ApplicationChannelCollection
{
    /// <summary>
    /// Ocelot网关
    /// </summary>
    public static readonly Channel GateWayOcelot = new(
        "aedf0ca1353c47dd9beb7a4ea34adc38",
        "GateWayOcelot",
        "Ocelot网关"
    );

    /// <summary>
    /// OneService
    /// </summary>
    public static readonly Channel OneService = new(
        "64a4d0c69aa248bb91b2e7f24416fab4",
        "OneService",
        "OneService"
    );
}