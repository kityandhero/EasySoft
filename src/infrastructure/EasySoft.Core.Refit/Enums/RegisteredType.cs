using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.Core.Refit.Enums;

/// <summary>
/// RegisteredType
/// </summary>
public enum RegisteredType
{
    /// <summary>
    /// direct
    /// </summary>
    [Alias("direct")]
    Direct = 100,

    /// <summary>
    /// consul
    /// </summary>
    [Alias("consul")]
    Consul = 200,

    /// <summary>
    /// nacos
    /// </summary>
    [Alias("nacos")]
    Nacos = 300,

    /// <summary>
    /// clusterIp
    /// </summary>
    [Alias("clusterIp")]
    ClusterIP = 400
}