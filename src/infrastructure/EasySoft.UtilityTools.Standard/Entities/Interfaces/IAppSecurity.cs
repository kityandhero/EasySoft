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
}