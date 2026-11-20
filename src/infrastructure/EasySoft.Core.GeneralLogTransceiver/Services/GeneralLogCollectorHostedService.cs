using EasySoft.Core.GeneralLogTransceiver.Collectors;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.GeneralLogTransceiver.Services;

/// <summary>
/// 日志收集器托管后台服务
/// 作用：将 GeneralLogCollector 纳入 ASP.NET Core Host 生命周期托管，解决裸Task游离、停止兜底刷写时序不一致问题
/// 对比直接在 ApplicationStarted 手动Start：Host会识别该服务，停止阶段主动等待 StopAsync 完成，受 HostOptions.ShutdownTimeout 管控
/// </summary>
public class GeneralLogCollectorHostedService : BackgroundService
{
    private readonly GeneralLogCollector _collector;

    /// <summary>
    /// 日志收集器托管后台服务
    /// </summary>
    /// <param name="collector">收集器实例，构造阶段仅初始化，尚未启动后台循环</param>
    public GeneralLogCollectorHostedService(GeneralLogCollector collector)
    {
        _collector = collector;
    }

    /// <summary>
    /// Host后台执行入口，宿主依赖全部就绪后执行
    /// </summary>
    /// <param name="stoppingToken">宿主停止令牌，Host关闭时自动取消</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 所有依赖注入就绪后主动Start
        _collector.Start();

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    /// <summary>
    /// Host优雅关闭回调，收到停止信号优先关闭收集器
    /// </summary>
    /// <param name="stoppingToken">停止令牌，超时后宿主会强制终止</param>
    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _collector.Close();

        return base.StopAsync(stoppingToken);
    }
}