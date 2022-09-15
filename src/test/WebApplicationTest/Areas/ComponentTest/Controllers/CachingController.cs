using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// CachingController
/// </summary>
public class CachingController : AreaControllerCore
{
    private const string TestKey = "testKey";
    private const string TestSlidingKey = "testSlidingKey";
    private const string TestObjectKey = "testObjectKey";

    private const string TestAsyncKey = "testAsyncKey";
    private const string TestAsyncSlidingKey = "testAsyncSlidingKey";
    private const string TestAsyncObjectKey = "testAsyncObjectKey";

    private readonly IAsyncCacheOperator _cacheOperator;

    /// <summary>
    /// CachingController
    /// </summary>
    /// <param name="cacheOperator"></param>
    public CachingController(IAsyncCacheOperator cacheOperator)
    {
        _cacheOperator = cacheOperator;
    }

    #region 同步模式简单值测试

    /// <summary>
    /// Set
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult Set(string value = "test", string key = TestKey)
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

    #endregion

    #region 同步模式类实例值测试

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

    #endregion

    #region 同步滑动模式测试

    /// <summary>
    /// SetSliding
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult SetSliding(string value = "test", string key = TestSlidingKey)
    {
        _cacheOperator.Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * 60),
            new TimeSpan(TimeSpan.TicksPerSecond * 30)
        );

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// GetSliding
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult GetSliding(string key = TestSlidingKey)
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

    #endregion

    #region 异步模式简单值测试

    /// <summary>
    /// AsyncSet
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<IActionResult> AsyncSet(string value = "test", string key = TestAsyncKey)
    {
        await _cacheOperator.SetAsync(key, value, new TimeSpan(TimeSpan.TicksPerSecond * 360));

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// AsyncGet
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<IActionResult> AsyncGet(string key = TestAsyncKey)
    {
        var result = await _cacheOperator.GetAsync<string>(key);

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

    #endregion

    #region 异步模式类实例值测试

    /// <summary>
    /// AsyncSetObject
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<IActionResult> AsyncSetObject(string key = TestAsyncObjectKey)
    {
        var value = new Hello
        {
            Name = "Jon",
            Date = DateTime.Now
        };

        await _cacheOperator.SetAsync(key, value, new TimeSpan(TimeSpan.TicksPerSecond * 360));

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// AsyncGetObject
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<IActionResult> AsyncGetObject(string key = TestAsyncObjectKey)
    {
        var result = await _cacheOperator.GetAsync<Hello>(key);

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

    #endregion

    #region 异步滑动模式测试

    /// <summary>
    /// AsyncSetSliding
    /// </summary>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<IActionResult> AsyncSetSliding(string value = "test", string key = TestAsyncSlidingKey)
    {
        await _cacheOperator.SetAsync(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * 60),
            new TimeSpan(TimeSpan.TicksPerSecond * 30)
        );

        return this.Success(new
        {
            value,
            time = DateTime.Now.ToUnixTime()
        });
    }

    /// <summary>
    /// AsyncGetSliding
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<IActionResult> AsyncGetSliding(string key = TestAsyncSlidingKey)
    {
        var result = await _cacheOperator.GetAsync<string>(key);

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

    #endregion
}