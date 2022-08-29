using Autofac;
using AutoMapper;
using EasySoft.Core.AutoFac.IocAssists;

namespace EasySoft.Core.AutoMapper.Assists;

public static class MapperAssist
{
    public static IMapper GetMapper()
    {
        if (AutofacAssist.Instance.GetContainer().IsRegistered<IMapper>())
        {
            throw new Exception("AutoMap has not been injected, please use AddAdvanceAutoMapper before use GetMapper");
        }

        return AutofacAssist.Instance.Resolve<IMapper>();
    }
}