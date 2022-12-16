using EasySoft.UtilityTools.Standard.Extensions;

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

        if (!GeneralConfigAssist.GetConfigCenterSwitch()) return defaultTokenExpires;

        var remoteConfigCache = AgileConfigClientAssist.GetConfigClient().Data;

        var remoteTokenExpires = remoteConfigCache[ConstCollection.TokenExpiresKey] ?? "";

        if (remoteTokenExpires.IsInt(out var value) && value > 0) return value;

        return defaultTokenExpires;
    }

    public static ExecutiveResult<string> GetNLogJsonConfig()
    {
        try
        {
            string remoteNLogJsonConfig;

            if (!GeneralConfigAssist.GetConfigCenterSwitch())
                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = ""
                };

            if (GeneralConfigAssist.GetConfigCenterType() == ConfigCenterType.AgileConfig)
                remoteNLogJsonConfig = GetJsonConfigFromAgileConfig();
            else if (GeneralConfigAssist.GetConfigCenterType() == ConfigCenterType.Consul)
                remoteNLogJsonConfig = GetJsonConfigFromConsul();
            else
                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = ""
                };

            if (string.IsNullOrWhiteSpace(remoteNLogJsonConfig))
                return new ExecutiveResult<string>(ReturnCode.Ok)
                {
                    Data = ""
                };

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

    private static string GetJsonConfigFromAgileConfig()
    {
        var remoteConfigCache = AgileConfigClientAssist.GetConfigClient().Data;

        if (!remoteConfigCache.Keys.Contains(ConstCollection.NLogJsonConfig))
        {
            LogAssist.Warning(
                "The “NLogJsonConfig” int the remote do not exist, will use local config."
            );

            return "";
        }

        return remoteConfigCache[ConstCollection.NLogJsonConfig] ?? "";
    }

    private static string GetJsonConfigFromConsul()
    {
        try
        {
            var applicationChannel = AutofacAssist.Instance.Resolve<IApplicationChannel>();

            var consulClient = ConsulConfigCenterClientAssist.GetConfigClient();

            var v = consulClient.KV.Get(
                $"{applicationChannel.GetChannel()}/config.{EnvironmentAssist.GetEnvironment().EnvironmentName}.json"
            ).Result;

            var config = Encoding.UTF8.GetString(v.Response.Value, 0, v.Response.Value.Length);

            return config;
        }
        catch (Exception)
        {
            return "";
        }
    }
}