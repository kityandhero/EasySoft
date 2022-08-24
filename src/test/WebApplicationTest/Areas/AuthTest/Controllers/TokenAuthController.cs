using EasySoft.Core.AuthenticationCore.Attributes;
using EasySoft.Core.AuthenticationCore.ExtensionMethods;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.DynamicConfig.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.AuthTest.Controllers;

public class TokenAuthController : AreaControllerCore
{
    public IActionResult GenerateToken()
    {
        var token = 123456.ToToken();

        return this.Success(new
        {
            tokenMode = FlagAssist.TokenMode,
            token,
            expires = DynamicConfigAssist.GetTokenExpires(),
            tokenServerDump = GeneralConfigAssist.GetTokenServerDumpSwitch(),
            tokenParseFromUrlSwitch = GeneralConfigAssist.GetTokenParseFromUrlSwitch(),
            tokenParseFromCookieSwitch = GeneralConfigAssist.GetTokenParseFromCookieSwitch(),
        });
    }

    public IActionResult GetToken()
    {
        var token = HttpContext.GetToken();

        return this.Success(new
        {
            tokenMode = FlagAssist.TokenMode,
            token,
            expires = DynamicConfigAssist.GetTokenExpires(),
            tokenServerDump = GeneralConfigAssist.GetTokenServerDumpSwitch(),
            tokenParseFromUrlSwitch = GeneralConfigAssist.GetTokenParseFromUrlSwitch(),
            tokenParseFromCookieSwitch = GeneralConfigAssist.GetTokenParseFromCookieSwitch(),
        });
    }

    [Operator]
    public IActionResult NeedAuth()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// 错误配置示例：匿名用户不支持鉴权, 请修复程序（配置登录验证）
    /// </summary>
    /// <returns></returns>
    [GuidTag("356316bbf81e4cda93ab9a1238765878")]
    public IActionResult ConfigErrorPermission()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }

    [Operator]
    [GuidTag("356316bbf81e4cda93ab9a1238765875")]
    public IActionResult NeedPermission()
    {
        return this.Success(new
        {
            time = DateTime.Now.ToUnixTime()
        });
    }
}