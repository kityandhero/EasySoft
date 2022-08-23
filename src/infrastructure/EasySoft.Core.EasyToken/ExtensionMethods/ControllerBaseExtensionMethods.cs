using EasySoft.Core.EasyToken.AccessControl;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class ControllerBaseExtensionMethods
{
    public static string GetToken(this ControllerBase c)
    {
        return c.Request.Headers.ContainsKey("token") ? c.Request.Headers["token"] : "";
    }

    public static AccessKey? GetFromHttpHeaderWithSecret(this ControllerBase c, Secret secret)
    {
        var token = GetToken(c);

        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        try
        {
            var info = secret.DecryptWithExpirationTime(token, out var expired);

            if (!expired)
            {
                return AccessAssist.GetFromSimpleInfoWithSecret(info, secret);
            }
        }
        catch (Exception)
        {
            return null;
        }

        return null;
    }

    public static AccessKey? GetFromParamWithSecret(this ControllerBase c, Secret secret)
    {
        var integratedParams = c.GetIntegratedParams();

        if (!integratedParams.AllKeys.Contains("token"))
        {
            return null;
        }

        var token = integratedParams["token"] ?? "";

        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        try
        {
            var info = secret.DecryptWithExpirationTime(token, out var expired);

            if (!expired)
            {
                return AccessAssist.GetFromSimpleInfoWithSecret(info, secret);
            }
        }
        catch (Exception)
        {
            return null;
        }

        return null;
    }
}