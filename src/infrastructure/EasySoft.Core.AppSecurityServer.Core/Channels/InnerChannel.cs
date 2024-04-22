using EasySoft.UtilityTools.Standard.Interfaces;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.AppSecurityServer.Core.Channels;

/// <summary>
/// 
/// </summary>
public static class InnerChannel
{
    /// <summary>
    /// 应用安全主控器
    /// </summary>
    public static readonly IChannel AppSecurityServer = new Channel(
        "6cac6cddb313423782aa98f55f6d1f42",
        "AppSecurityServer",
        "应用安全主控器"
    );
}