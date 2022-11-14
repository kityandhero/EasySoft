namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

public class RegisterDto : IApiParams
{
    /// <summary>
    /// 登录名
    /// </summary>
    public string LoginName { get; set; }

    /// <summary>
    /// 登陆密码
    /// </summary>
    public string Password { get; set; }

    public RegisterDto()
    {
        LoginName = string.Empty;
        Password = string.Empty;
    }
}