using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.Web.Framework.Controllers;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers;

public class CachingController : CustomControllerBase
{
    private const string TestKey = "testKey";
    private const string TestObjectKey = "testObjectKey";

    private readonly ICacheOperator _cacheOperator;

    public CachingController(ICacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    public IActionResult Set(string value, string key = TestKey)
    {
        _cacheOperator.Set(key, value, new TimeSpan(TimeSpan.TicksPerSecond * 360));

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    public IActionResult Get(string key = TestKey)
    {
        var result = _cacheOperator.Get<string>(key);

        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(new
        {
            cacheMode = GeneralConfigAssist.GetCacheMode(),
            value = result.Data,
            time = DateTime.Now.ToUnixTime()
        });
    }

    public IActionResult SetObject(string key = TestObjectKey)
    {
        var value = new Hello
        {
            Name = "Jon",
            Date = DateTime.Now
        };

        _cacheOperator.Set(key, value, new TimeSpan(TimeSpan.TicksPerSecond * 360));

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    public IActionResult GetObject(string key = TestObjectKey)
    {
        var result = _cacheOperator.Get<Hello>(key);

        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(new
        {
            cacheMode = GeneralConfigAssist.GetCacheMode(),
            value = result.Data,
            time = DateTime.Now.ToUnixTime()
        });
    }
}