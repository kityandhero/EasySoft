namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// app security
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