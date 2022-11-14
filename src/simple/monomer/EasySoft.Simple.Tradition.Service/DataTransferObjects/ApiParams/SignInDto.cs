using System.ComponentModel;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Params;

namespace EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;

public class SignInDto : IApiParams
{
    /// <summary>
    /// 登录名
    /// </summary>
    [DisplayName("loginName")]
    [Require]
    public string LoginName { get; set; }

    /// <summary>
    /// 登陆密码
    /// </summary>
    [DisplayName("password")]
    [Require]
    public string Password { get; set; }

    public SignInDto()
    {
        LoginName = string.Empty;
        Password = string.Empty;
    }
}