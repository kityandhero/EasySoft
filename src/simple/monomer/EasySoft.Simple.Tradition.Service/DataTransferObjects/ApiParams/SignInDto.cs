namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

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

    public SignInDto()
    {
        LoginName = string.Empty;
        Password = string.Empty;
    }
}