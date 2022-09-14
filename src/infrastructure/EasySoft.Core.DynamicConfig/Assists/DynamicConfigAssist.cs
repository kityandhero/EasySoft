using EasySoft.Core.AgileConfigClient.Assists;
using EasySoft.Core.Config;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Core.DynamicConfig.Assists;

public static class DynamicConfigAssist
{
    /// <summary>
    /// 过期时间 (秒), 默认7200
    /// </summary> 
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int GetTokenExpires()
    {
        var defaultTokenExpires = GeneralConfigAssist.GetTokenExpires();

        if (!GeneralConfigAssist.GetConfigCenterSwitch())
        {
            return defaultTokenExpires;
        }

        var remoteConfigCache = AgileConfigClientAssist.GetConfigClient().Data;

        var remoteTokenExpires = remoteConfigCache[ConstCollection.TokenExpiresKey] ?? "";

        if (remoteTokenExpires.IsInt(out var value) && value > 0)
        {
            return value;
        }

        return defaultTokenExpires;
    }

    public static ExecutiveResult<string> GetNLogJsonConfig()
    {
        try
        {
            var remoteConfigCache = AgileConfigClientAssist.GetConfigClient().Data;

            if (!remoteConfigCache.Keys.Contains(ConstCollection.NLogJsonConfig))
            {
                LogAssist.Warning(
                    "The “NLogJsonConfig” int the remote do not exist, will use local config."
                );

                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = ""
                };
            }

            var remoteNLogJsonConfig = remoteConfigCache[ConstCollection.NLogJsonConfig] ?? "";

            if (string.IsNullOrWhiteSpace(remoteNLogJsonConfig))
            {
                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = ""
                };
            }

            try
            {
                var o = JsonConvertAssist.DeserializeObject<object>(remoteNLogJsonConfig);

                if (o == null)
                {
                    LogAssist.Error(
                        "The “NLogJsonConfig” received not in JSON format, please check in agileConfig node, will use local config."
                    );

                    return new ExecutiveResult<string>(ReturnCode.Ok)
                    {
                        Data = ""
                    };
                }

                if (JsonConvertAssist.Serialize(o) == "{}")
                {
                    LogAssist.Warning(
                        "The “NLogJsonConfig” received dot not have any config items, config will not accept it, please check in agileConfig node, will use local config."
                    );

                    return new ExecutiveResult<string>(ReturnCode.Ok)
                    {
                        Data = ""
                    };
                }

                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = remoteNLogJsonConfig
                };
            }
            catch (Exception)
            {
                LogAssist.Error(
                    "The “NLogJsonConfig” received not in JSON format, please check in agileConfig node, will use local config."
                );

                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = ""
                };
            }
        }
        catch (Exception e)
        {
            LogAssist.Error(
                $"Received “NLogJsonConfig” from remote cause some error, message: \"{e.Message}\", will use local config."
            );

            return new ExecutiveResult<string>(ReturnCode.Ok)
            {
                Data = ""
            };
        }
    }
}