namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// IAppSecurity
/// </summary>
public interface IAppSecurity
{
    /// <summary>
    /// AppId
    /// </summary>
    [Description("AppId")]
    string AppId { get; set; }

    /// <summary>
    /// 应用密钥
    /// </summary>
    [Description("AppSecret")]
    string AppSecret { get; set; }

    /// <summary>
    /// 主控应用，主控应用全局仅限一个，且与 Channel 无关
    /// </summary>
    [Description("MasterControl")]
    int MasterControl { get; set; }
}