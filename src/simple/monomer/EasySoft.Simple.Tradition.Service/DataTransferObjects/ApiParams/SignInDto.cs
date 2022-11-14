namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

public class SignInDto : IApiParams
{
    /// <summary>
    /// 登录名
    /// </summary>
    [DisplayName("loginName")]
    [Required]
    public string LoginName { get; set; }

    /// <summary>
    /// 登陆密码
    /// </summary>
    [DisplayName("password")]
    [Required]
    public string Password { get; set; }

    public SignInDto()
    {
        LoginName = string.Empty;
        Password = string.Empty;
    }
}