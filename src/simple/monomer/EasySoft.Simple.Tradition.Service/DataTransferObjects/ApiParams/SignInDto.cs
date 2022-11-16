namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

/// <summary>
/// SignInDto
/// </summary>
public class SignInDto : IApiParams
{
    /// <summary>
    /// 登录名
    /// </summary>
    [Required]
    public string LoginName { get; set; }

    /// <summary>
    /// 登陆密码
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// SignInDto
    /// </summary>
    public SignInDto()
    {
        LoginName = string.Empty;
        Password = string.Empty;
    }
}