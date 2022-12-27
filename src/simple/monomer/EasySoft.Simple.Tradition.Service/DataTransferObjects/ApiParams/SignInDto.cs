using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

/// <summary>
/// SignInDto
/// </summary>
public class SignInDto : IApiParams, IAccount
{
    /// <summary>
    /// 登录名
    /// </summary>
    [Required]
    public string AccountName { get; set; } = string.Empty;

    /// <summary>
    /// 登陆密码
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}