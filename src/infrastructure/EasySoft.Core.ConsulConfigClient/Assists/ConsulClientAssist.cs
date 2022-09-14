using Consul;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Config.ConfigAssist;

namespace EasySoft.Core.ConsulConfigClient.Assists;

public static class ConsulClientAssist
{
    private static readonly ConsulClient ConsulClient;

    static ConsulClientAssist()
    {
        ConsulClient = new ConsulClient(x => { x.Address = new Uri(ConsulConfigAssist.GetConsulAddress()); });
    }

    public static IConsulClient GetConfigClient()
    {
        return ConsulClient;
    }
}