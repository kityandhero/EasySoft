using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Core.Mvc.Framework.Controllers;
using EasySoft.Core.Mvc.Framework.ExtensionMethods;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class CachingController : CustomControllerBase
{
    private const string TestKey = "testKey";

    private readonly ICacheOperator _cacheOperator;

    public CachingController(ICacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    public IActionResult Set(string value)
    {
        _cacheOperator.Set(TestKey, value, new TimeSpan(TimeSpan.TicksPerSecond * 360));

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    public IActionResult Get()
    {
        var result = _cacheOperator.Get<string>(TestKey);

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