using Consul;
using EasySoft.Core.AutoFac.IocAssists;

namespace EasySoft.Core.ConsulConfigClient.Assists;

public static class ConsulClientAssist
{
    public static IConsulClient GetConfigClient()
    {
        return AutofacAssist.Instance.Resolve<IConsulClient>();
    }
}