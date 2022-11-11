namespace EasySoft.Core.AuthenticationCore.ExtensionMethods;

public static class HttpRequestExtensions
{
    public static string GetToken(this HttpRequest httpRequest, string key = "token")
    {
        var token = httpRequest.Headers.ContainsKey(key) ? httpRequest.Headers[key].ToString() : "";

        if (!string.IsNullOrWhiteSpace(token)) return ExchangeRealToken(token);

        if (GeneralConfigAssist.GetTokenParseFromUrlSwitch() && httpRequest.Query.ContainsKey(key))
        {
            token = httpRequest.Query[key];

            if (!string.IsNullOrWhiteSpace(token)) return ExchangeRealToken(token);
        }

        if (GeneralConfigAssist.GetTokenParseFromCookieSwitch() && httpRequest.Cookies.ContainsKey(key))
        {
            token = httpRequest.Cookies[key];

            if (!string.IsNullOrWhiteSpace(token)) return ExchangeRealToken(token);
        }

        return token ?? "";
    }

    public static async Task<string> GetTokenAsync(this HttpRequest httpRequest, string key = "token")
    {
        var token = httpRequest.Headers.ContainsKey(key) ? httpRequest.Headers[key].ToString() : "";

        if (!string.IsNullOrWhiteSpace(token)) return await ExchangeRealTokenAsync(token);

        if (GeneralConfigAssist.GetTokenParseFromUrlSwitch() && httpRequest.Query.ContainsKey(key))
        {
            token = httpRequest.Query[key];

            if (!string.IsNullOrWhiteSpace(token)) return await ExchangeRealTokenAsync(token);
        }

        if (GeneralConfigAssist.GetTokenParseFromCookieSwitch() && httpRequest.Cookies.ContainsKey(key))
        {
            token = httpRequest.Cookies[key];

            if (!string.IsNullOrWhiteSpace(token)) return await ExchangeRealTokenAsync(token);
        }

        return token ?? "";
    }

    private static string ExchangeRealToken(string tokenOrKey)
    {
        if (!GeneralConfigAssist.GetTokenServerDumpSwitch()) return tokenOrKey;

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var result = asyncCacheOperator.Get<string>(tokenOrKey);

        if (result.Success) return result.Data ?? "";

        return "";
    }

    private static async Task<string> ExchangeRealTokenAsync(string tokenOrKey)
    {
        if (!GeneralConfigAssist.GetTokenServerDumpSwitch()) return tokenOrKey;

        var asyncCacheOperator = AutofacAssist.Instance.Resolve<IAsyncCacheOperator>();

        var result = await asyncCacheOperator.GetAsync<string>(tokenOrKey);

        if (result.Success) return result.Data ?? "";

        return "";
    }
}