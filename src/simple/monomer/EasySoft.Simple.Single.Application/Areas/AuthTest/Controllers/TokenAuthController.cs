﻿namespace EasySoft.Simple.Single.Application.Areas.AuthTest.Controllers;

/// <summary>
/// TokenAuth
/// </summary>
public class TokenAuthController : AreaControllerCore
{
    /// <summary>
    /// GenerateToken
    /// </summary>
    /// <returns></returns>
    public IActionResult GenerateToken()
    {
        var token = 123456.ToToken();

        return this.Success(
            new
            {
                tokenMode = FlagAssist.TokenMode,
                token,
                expires = DynamicConfigAssist.GetTokenExpires(),
                tokenServerDump = GeneralConfigAssist.GetTokenServerDumpSwitch(),
                tokenParseFromUrlSwitch = GeneralConfigAssist.GetTokenParseFromUrlSwitch(),
                tokenParseFromCookieSwitch = GeneralConfigAssist.GetTokenParseFromCookieSwitch()
            }
        );
    }

    /// <summary>
    /// GetToken
    /// </summary>
    /// <returns></returns>
    public IActionResult GetToken()
    {
        var token = HttpContext.GetToken();

        return this.Success(
            new
            {
                tokenMode = FlagAssist.TokenMode,
                token,
                expires = DynamicConfigAssist.GetTokenExpires(),
                tokenServerDump = GeneralConfigAssist.GetTokenServerDumpSwitch(),
                tokenParseFromUrlSwitch = GeneralConfigAssist.GetTokenParseFromUrlSwitch(),
                tokenParseFromCookieSwitch = GeneralConfigAssist.GetTokenParseFromCookieSwitch()
            }
        );
    }

    /// <summary>
    /// NeedAuth
    /// </summary>
    /// <returns></returns>
    [Operator]
    public IActionResult NeedAuth()
    {
        return this.Success(
            new
            {
                time = DateTime.Now.ToUnixTime()
            }
        );
    }

    /// <summary>
    /// 错误配置示例：匿名用户不支持鉴权, 请修复程序（配置登录验证）
    /// </summary>
    /// <returns></returns>
    [Permission("356316bbf81e4cda93ab9a1238765878")]
    public IActionResult ConfigErrorPermission()
    {
        return this.Success(
            new
            {
                time = DateTime.Now.ToUnixTime()
            }
        );
    }

    /// <summary>
    /// NeedPermission
    /// </summary>
    /// <returns></returns>
    [Operator]
    [Permission("356316bbf81e4cda93ab9a1238765875")]
    public IActionResult NeedPermission()
    {
        return this.Success(
            new
            {
                time = DateTime.Now.ToUnixTime()
            }
        );
    }
}