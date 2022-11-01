using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Mapster.Assists;
using EasySoft.Simple.Shared.Application.DataTransfer;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// AutoMapperController
/// </summary>
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

    /// <summary>
    /// AutoMapperController
    /// </summary>
    /// <param name="mapper"></param>
    public AutoMapperController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return Content("autoMapper test");
    }

    /// <summary>
    /// TestOne
    /// </summary>
    /// <returns></returns>
    public IActionResult TestOne()
    {
        var userDto = _userEntity.Adapt<UserDto>();

        _userEntity.Adapt(userDto);

        return this.Success(userDto);
    }

    /// <summary>
    /// TestTwo
    /// </summary>
    /// <returns></returns>
    public IActionResult TestTwo()
    {
        var userDto = _mapper.Map<UserDto>(_userEntity);

        return this.Success(userDto);
    }

    /// <summary>
    /// TestThree
    /// </summary>
    /// <returns></returns>
    public IActionResult TestThree()
    {
        var mapper = MapperAssist.GetMapper();

        var userDto = mapper.Map<UserDto>(_userEntity);

        return this.Success(userDto);
    }

    /// <summary>
    /// TestFour
    /// </summary>
    /// <returns></returns>
    public IActionResult TestFour()
    {
        var userIn = new UserIn()
        {
            Name = "Snow",
            Age = 20
        };

        var userDto1 = _userEntity.BuildAdapter()
            .AddParameters("Age", userIn.Age)
            .AdaptToType<UserDto>();

        var userDto2 = _mapper.From(_userEntity)
            .AddParameters("name", userIn.Name)
            .AddParameters("age", userIn.Age)
            .AdaptToType<UserDto>();

        return this.Success(new
        {
            userDto1,
            userDto2
        });
    }

    /// <summary>
    /// TestOther
    /// </summary>
    /// <returns></returns>
    public IActionResult TestOther()
    {
        //equal to (decimal)123;
        var i = 123.Adapt<decimal>();

        // 字符串转枚举，如果字符串为空或空字符串，那么枚举将初始化为第一个枚举值。
        var e = "Read, Write, Delete".Adapt<FileShare>();
        // FileShare.Read | FileShare.Write | FileShare.Delete

        var s = 123.Adapt<string>(); // 等同于: 123.ToString();
        var i2 = "123".Adapt<int>(); // 等同于: int.Parse("123");

        // 集合, 包括列表、数组、集合、包括各种接口的字典之间的映射: IList<T> , ICollection<T >, IEnumerable<T> , ISet<T >, IDictionary<TKey, TValue> 等等…
        var list = new UserEntity[] { _userEntity }.ToList();
        var target = list.Adapt<IEnumerable<UserDto>>();

        // 可映射对象 Mapster 可以使用以下规则映射两个不同的对象 源类型和目标类型属性名称相同。 例如: dest.Name = src.Name
        // 源类型有 GetXXXX 方法。例如: dest.Name = src.GetName()
        // 源类型属性有子属性，可以将子属性的赋值给符合条件的目标类型属性，例如: dest.ContactName = src.Contact.Name 或 dest.Contact_Name = src.Contact.Name

        // 可映射对象类型包括: 类 结构体 接口 实现 IDictionary<string, T> 接口的字典类型 Record 类型 (类、结构体、接口)

        return this.Success();
    }
}