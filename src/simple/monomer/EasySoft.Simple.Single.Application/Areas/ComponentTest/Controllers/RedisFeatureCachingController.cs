using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.EasyCaching.interfaces;
using EasySoft.Simple.Single.Application.Models;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// RedisFeatureCachingController
/// </summary>
public class RedisFeatureCachingController : AreaControllerCore
{
    private const string TestKey = "testKey";
    private const string TestObjectKey = "testObjectKey";

    private readonly IRedisFeatureCacheOperator _cacheOperator;

    /// <summary>
    /// RedisFeatureCachingController
    /// </summary>
    /// <param name="cacheOperator"></param>
    public RedisFeatureCachingController(IRedisFeatureCacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult Set(string value, string key = TestKey)
    {
        _cacheOperator.Set(key, value, new TimeSpan(TimeSpan.TicksPerSecond * 360));

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult Get(string key = TestKey)
    {
        var result = _cacheOperator.Get<string>(key);

        if (!result.Success) return this.Fail(result.Code);

        return this.Success(new
        {
            cacheMode = GeneralConfigAssist.GetCacheMode(),
            value = result.Data,
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// SetObject
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
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

    /// <summary>
    /// GetObject
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult GetObject(string key = TestObjectKey)
    {
        var result = _cacheOperator.Get<Hello>(key);

        if (!result.Success) return this.Fail(result.Code);

        return this.Success(new
        {
            cacheMode = GeneralConfigAssist.GetCacheMode(),
            value = result.Data,
            time = DateTime.Now.ToUnixTime()
        });
    }
}