﻿namespace EasySoft.Core.Refit.Assists;

/// <summary>
/// RefitAssist
/// </summary>
public static class RefitAssist
{
    /// <summary>
    /// GenerateRefitPolicies
    /// </summary>
    /// <returns></returns>
    public static List<IAsyncPolicy<HttpResponseMessage>> GenerateRefitPolicies()
    {
        //隔离策略
        //var bulkheadPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10, 100);

        //回退策略
        //回退也称服务降级，用来指定发生故障时的备用方案。
        //目前用不上
        //var fallbackPolicy = Policy<string>.Handle<HttpRequestException>().FallbackAsync("substitute data");

        //缓存策略
        //缓存策略无效
        //https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory?WT.mc_id=-blog-scottha#user-content-use-case-cachep
        //var cache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        //var cacheProvider = new MemoryCacheProvider(cache);
        //var cachePolicy = Policy.CacheAsync<HttpResponseMessage>(cacheProvider, TimeSpan.FromSeconds(100));

        //重试策略,超时或者API返回>500的错误,重试3次。
        //重试次数会统计到失败次数
        var retryPolicy = Policy.Handle<TimeoutRejectedException>()
            .OrResult<HttpResponseMessage>(response => (int)response.StatusCode >= 500)
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5)
            });

        //超时策略
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(EnvironmentAssist.IsDevelopment() ? 10 : 9);

        //熔断策略
        //如下，如果我们的业务代码连续失败50次，就触发熔断(onBreak),就不会再调用我们的业务代码，而是直接抛出BrokenCircuitException异常。
        //当熔断时间10分钟后(durationOfBreak)，切换为HalfOpen状态，触发onHalfOpen事件，此时会再调用一次我们的业务代码
        //，如果调用成功，则触发onReset事件，并解除熔断，恢复初始状态，否则立即切回熔断状态。
        var circuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync
            (
                // 熔断前允许出现几次错误
                10
                ,
                // 熔断时间,熔断10分钟
                TimeSpan.FromMinutes(3)
                ,
                // 熔断时触发
                (ex, breakDelay) =>
                {
                    //todo
                    var e = ex;
                    var delay = breakDelay;
                }
                ,
                //熔断恢复时触发
                () =>
                {
                    //todo
                }
                ,
                //在熔断时间到了之后触发
                () =>
                {
                    //todo
                }
            );

        return new List<IAsyncPolicy<HttpResponseMessage>>()
        {
            retryPolicy,
            timeoutPolicy,
            circuitBreakerPolicy.AsAsyncPolicy<HttpResponseMessage>()
        };
    }
}