using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

public static class HttpContextExtensions
{
    public static string GetToken(this HttpContext c, string key = "token")
    {
        var token = c.Request.Headers.ContainsKey(key) ? c.Request.Headers[key].ToString() : "";

        if (!string.IsNullOrWhiteSpace(token))
        {
            return ExchangeRealToken(token);
        }

        if (GeneralConfigAssist.GetTokenParseFromUrlSwitch() && c.Request.Query.ContainsKey(key))
        {
            token = c.Request.Query[key];

            if (!string.IsNullOrWhiteSpace(token))
            {
                return ExchangeRealToken(token);
            }
        }

        if (GeneralConfigAssist.GetTokenParseFromCookieSwitch() && c.Request.Cookies.ContainsKey(key))
        {
            token = c.Request.Cookies[key];

            if (!string.IsNullOrWhiteSpace(token))
            {
                return ExchangeRealToken(token);
            }
        }

        return token ?? "";
    }

    public static async Task<string> GetTokenAsync(this HttpContext c, string key = "token")
    {
        var token = c.Request.Headers.ContainsKey(key) ? c.Request.Headers[key].ToString() : "";

        if (!string.IsNullOrWhiteSpace(token))
        {
            return await ExchangeRealTokenAsync(token);
        }

        if (GeneralConfigAssist.GetTokenParseFromUrlSwitch() && c.Request.Query.ContainsKey(key))
        {
            token = c.Request.Query[key];

            if (!string.IsNullOrWhiteSpace(token))
            {
                return await ExchangeRealTokenAsync(token);
            }
        }

        if (GeneralConfigAssist.GetTokenParseFromCookieSwitch() && c.Request.Cookies.ContainsKey(key))
        {
            token = c.Request.Cookies[key];

            if (!string.IsNullOrWhiteSpace(token))
            {
                return await ExchangeRealTokenAsync(token);
            }
        }

        return token ?? "";
    }

    private static string ExchangeRealToken(string tokenOrKey)
    {
        if (!GeneralConfigAssist.GetTokenServerDumpSwitch())
        {
            return tokenOrKey;
        }

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var result = asyncCacheOperator.Get<string>(tokenOrKey);

        if (result.Success)
        {
            return result.Data ?? "";
        }

        return "";
    }

    private static async Task<string> ExchangeRealTokenAsync(string tokenOrKey)
    {
        if (!GeneralConfigAssist.GetTokenServerDumpSwitch())
        {
            return tokenOrKey;
        }

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var result = await asyncCacheOperator.GetAsync<string>(tokenOrKey);

        if (result.Success)
        {
            return result.Data ?? "";
        }

        return "";
    }
}