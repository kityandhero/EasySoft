using AutoMapper;
using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.AutoMappers;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class AutoMapperController : AreaControllerCore
{
    private readonly IMapper _mapper;

    private readonly UserEntity _userEntity = new()
    {
        Name = "Jon",
        Gender = "male",
        Age = 18,
        Address = "NewYork"
    };

    // public AutoMapperController(IMapper mapper)
    // {
    //     _mapper = mapper;
    // }

    public IActionResult Index()
    {
        return Content("autoMapper test");
    }

    public IActionResult TestIn()
    {
        var sda = AutofacAssist.Instance.IsRegistered<MapperConfiguration>();
        
        var ss = AutofacAssist.Instance.Resolve<MapperConfiguration>();
        
        var userOut = AutofacAssist.Instance.Resolve<IMapper>().Map<UserOut>(_userEntity);

        return this.Success(userOut);
    }
}