using Consul;
using EasySoft.Core.Config.ConfigAssist;

namespace EasySoft.Core.ConsulClient.Assists;

public static class ConsulClientAssist
{
    private static readonly Consul.ConsulClient ConsulClient;

    static ConsulClientAssist()
    {
        ConsulClient = new Consul.ConsulClient(x =>
        {
            x.Address = new Uri(ConsulCenterConfigAssist.GetCenterAddress());
        });
    }

    public static IConsulClient GetConfigClient()
    {
        return ConsulClient;
    }
}