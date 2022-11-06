using EasySoft.UtilityTools.Standard.Params;

namespace EasySoft.Simple.AccountCenter.Application.Contracts.DataTransferObjects.ApiParams;

public class SignInDto : IApiParams
{
    /// <summary>
    /// 登录名
    /// </summary>
    public string LoginName { get; set; }

    /// <summary>
    /// 登陆密码
    /// </summary>
    public string Password { get; set; }

    public SignInDto()
    {
        LoginName = string.Empty;
        Password = string.Empty;
    }
}