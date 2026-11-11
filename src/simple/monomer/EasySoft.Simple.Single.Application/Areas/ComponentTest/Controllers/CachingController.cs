using EasySoft.Core.CacheCore.interfaces;
using EasySoft.Simple.Single.Application.Models;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

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

    private readonly ICacheOperator _cacheOperator;

    /// <summary>
    /// CachingController
    /// </summary>
    /// <param name="cacheOperator"></param>
    public CachingController(ICacheOperator cacheOperator)
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
        _cacheOperator.Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * 360)
        );

        return this.Success(
            new
            {
                value,
                time = DateTime.Now.ToUnixTime()
            }
        );
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult Get(string key = TestKey)
    {
        var result = _cacheOperator.GetSerializedValue(key);

        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(
            new
            {
                // cacheMode = GeneralConfigAssist.GetCacheMode(),
                value = result.Data,
                time = DateTime.Now.ToUnixTime()
            }
        );
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

        _cacheOperator.Set(
            key,
            value,
            new TimeSpan(TimeSpan.TicksPerSecond * 360)
        );

        return this.Success(
            new
            {
                value,
                time = DateTime.Now.ToUnixTime()
            }
        );
    }

    /// <summary>
    /// GetObject
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult GetObject(string key = TestObjectKey)
    {
        var result = _cacheOperator.GetSerializedValue(key);

        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(
            new
            {
                // cacheMode = GeneralConfigAssist.GetCacheMode(),
                value = result.Data,
                time = DateTime.Now.ToUnixTime()
            }
        );
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
            1,
            new TimeSpan(TimeSpan.TicksPerSecond * 60),
            new TimeSpan(TimeSpan.TicksPerSecond * 30)
        );

        return this.Success(
            new
            {
                value,
                time = DateTime.Now.ToUnixTime()
            }
        );
    }

    /// <summary>
    /// GetSliding
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IActionResult GetSliding(string key = TestSlidingKey)
    {
        var result = _cacheOperator.GetSerializedValue(key);

        if (!result.Success)
        {
            return this.Fail(result.Code);
        }

        return this.Success(
            new
            {
                // cacheMode = GeneralConfigAssist.GetCacheMode(),
                value = result.Data,
                time = DateTime.Now.ToUnixTime()
            }
        );
    }

    #endregion
}