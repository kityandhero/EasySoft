using Consul;
using EasySoft.Core.Config.ConfigAssist;

namespace EasySoft.Core.ConsulConfigCenterClient.Assists;

public static class ConsulConfigCenterClientAssist
{
    private static readonly ConsulClient ConsulClient;

    static ConsulConfigCenterClientAssist()
    {
        ConsulClient =
            new ConsulClient(x => { x.Address = new Uri(ConsulConfigCenterConfigAssist.GetCenterAddress()); });
    }

    public static IConsulClient GetConfigClient()
    {
        return ConsulClient;
    }
}