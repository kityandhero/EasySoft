using Consul;
using EasySoft.Core.Config.ConfigAssist;

namespace EasySoft.Core.ConsulRegistrationCenterClient.Assists;

internal static class ConsulRegistrationCenterClientAssist
{
    internal static IConsulClient GetConfigClient()
    {
        return new ConsulClient(x => { x.Address = new Uri(ConsulRegistrationCenterConfigAssist.GetCenterAddress()); });
    }
}