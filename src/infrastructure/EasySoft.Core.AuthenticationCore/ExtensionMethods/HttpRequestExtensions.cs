using EasySoft.Core.AuthenticationCore.Assists;
using EasySoft.Core.Infrastructure.Assists;

namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

/// <summary>
/// HttpRequestExtensions
/// </summary>
public static class HttpRequestExtensions
{
    /// <summary>
    /// GetToken
    /// </summary>
    /// <param name="httpRequest"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetToken(this HttpRequest httpRequest, string key = "token")
    {
        var token = httpRequest.Headers.ContainsKey(key) ? httpRequest.Headers[key].ToString() : "";

        if (!string.IsNullOrWhiteSpace(token))
        {
            return ExchangeRealToken(token);
        }

        var authenticationConfigureAssist = ServiceProviderAssist.Resolve<AuthenticationConfigureAssist>();

        var tokenParseFromUrlSwitchConfigValue = authenticationConfigureAssist.GetTokenParseFromUrlSwitch();

        if (tokenParseFromUrlSwitchConfigValue.Value && httpRequest.Query.ContainsKey(key))
        {
            token = httpRequest.Query[key];

            if (!string.IsNullOrWhiteSpace(token))
            {
                return ExchangeRealToken(token);
            }
        }

        var tokenParseFromCookieSwitchConfigValue = authenticationConfigureAssist.GetTokenParseFromCookieSwitch();

        if (tokenParseFromCookieSwitchConfigValue.Value && httpRequest.Cookies.ContainsKey(key))
        {
            token = httpRequest.Cookies[key];

            if (!string.IsNullOrWhiteSpace(token))
            {
                return ExchangeRealToken(token);
            }
        }

        return token ?? "";
    }

    private static string ExchangeRealToken(string tokenOrKey)
    {
        var authenticationConfigureAssist = ServiceProviderAssist.Resolve<AuthenticationConfigureAssist>();

        var tokenServerDumpSwitchConfigValue = authenticationConfigureAssist.GetTokenServerDumpSwitch();

        if (!tokenServerDumpSwitchConfigValue.Value)
        {
            return tokenOrKey;
        }

        var cacheOperator = ServiceProviderAssist.Resolve<ICacheOperator>();

        var result = cacheOperator.GetSerializedValue(tokenOrKey);

        if (result.Success)
        {
            return result.Data ?? "";
        }

        return "";
    }
}