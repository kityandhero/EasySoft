using EasySoft.UtilityTools.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.AdvanceFileRecord;

/// <summary>
/// 按日期滚动 UTF-8 文件记录器
/// </summary>
/// <remarks>
/// 特性：
/// 1. 缓冲批量写入，减少系统调用；定时/满缓冲自动刷盘
/// 2. 刷盘时才打开文件流，写完立刻释放句柄，不长期占用文件
/// 3. 支持原子临时文件rename，避免外部读到残缺行
/// 4. 内置目录自维护：容量上限 + 文件过期淘汰，独立冷却控制
/// 5. 磁盘水位保护，防止打满磁盘
/// 6. 线程安全：内部自带锁，多线程直接调用Write / AppendRaw
/// 7. 可释放：Dispose时强制刷盘，保证内存残留数据落地
/// 8. 无日志框架绑定，通用流式记录组件，适合快照、流水、自定义数据存储
/// 修复点：
/// - 补充 PeriodicTimer 独立周期巡检，解决【无新写入时超时不刷盘】硬伤
/// - 删除异步刷盘接口，规避 lock + await 非法竞态
/// - 刷盘无论成败更新 _lastFlush，防止IO失败后定时器风暴重试
/// - PeriodicTimer 优雅等待退出，消除Dispose阶段回调逃逸丢数据风险
/// </remarks>
public sealed class DateRollingUtf8FileRecorder : IDisposable
{
    private readonly ILogger _logger;
    private readonly DateRollingUtf8FileRecorderOptions _options;
    private readonly IFileRecorderMetrics _metrics;

    /// <summary>
    /// 批量写入缓冲区
    /// </summary>
    private readonly ArrayBufferWriter<byte> _batchBuffer;

    /// <summary>
    /// 换行符字节，Write序列化对象后自动追加
    /// </summary>
    private readonly byte[] _newline = { (byte)'\n' };

    /// <summary>
    /// 实例锁，保护缓冲区、日期滚动、刷盘、状态变更
    /// </summary>
    private readonly Lock _sync = new();

    /// <summary>
    /// 定时巡检刷盘（.NET6+ PeriodicTimer，优雅退出）
    /// </summary>
    private readonly PeriodicTimer _autoFlushTimer;

    /// <summary>
    /// 定时器后台循环任务
    /// </summary>
    private Task _timerLoopTask;

    /// <summary>
    /// 当前正在写入的文件所属日期
    /// </summary>
    private DateTime _currentRollDate;

    /// <summary>
    /// 当前目标文件完整路径
    /// </summary>
    private string _currentFilePath;

    /// <summary>
    /// 上一次刷盘尝试UTC时间（成功/失败都更新，控频防风暴）
    /// </summary>
    private DateTimeOffset _lastFlush;

    /// <summary>
    /// 上一次目录维护扫描UTC时间，用于冷却控制
    /// </summary>
    private DateTimeOffset _lastScanTime = DateTimeOffset.MinValue;

    /// <summary>
    /// 释放标记
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// 构造记录器
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="options">记录器配置</param>
    /// <param name="metrics">指标采集，传null自动使用NullFileRecorderMetrics</param>
    public DateRollingUtf8FileRecorder(
        ILogger logger,
        IOptions<DateRollingUtf8FileRecorderOptions> options,
        IFileRecorderMetrics metrics = null
    )
    {
        _logger = logger;
        _options = options.Value;
        _metrics = metrics ?? new NullFileRecorderMetrics();

        _batchBuffer = new ArrayBufferWriter<byte>(32 * 1024);
        _currentRollDate = GetRollDate();
        _currentFilePath = ResolvePath(_currentRollDate);
        _lastFlush = DateTimeOffset.UtcNow;

        _autoFlushTimer = new PeriodicTimer(_options.FlushInterval);
        _timerLoopTask = RunTimerLoopAsync();
    }

    /// <summary>
    /// 获取当前滚动判定日期（UTC/本地时间由配置控制）
    /// </summary>
    private DateTime GetRollDate()
    {
        return _options.UseLocalTimeForRolling ? DateTime.Now.Date : DateTime.UtcNow.Date;
    }

    /// <summary>
    /// 根据日期解析路径模板
    /// </summary>
    private string ResolvePath(DateTime date)
    {
        return _options.PathTemplate.Replace("{Date:yyyyMMdd}", date.ToString("yyyyMMdd"));
    }

    /// <summary>
    /// 写入对象，自动追加换行
    /// </summary>
    /// <typeparam name="T">待序列化数据类型</typeparam>
    /// <param name="payload">数据对象</param>
    /// <param name="jsonSettings">Newtonsoft.Json序列化配置</param>
    public void Write<T>(T payload, JsonSerializerSettings jsonSettings)
    {
        var text = JsonConvert.SerializeObject(payload, jsonSettings);

        Write(text);
    }

    /// <summary>
    /// 写入文本，自动追加换行
    /// </summary>
    /// <param name="text">文本</param>
    public void Write(string text)
    {
        lock (_sync)
        {
            if (_disposed)
            {
                return;
            }

            // 磁盘压力校验，空间不足直接丢弃
            if (_options.EnableDiskPressureProtection && IsDiskLow())
            {
                _metrics.IncrementDiscardByDiskPressure();

                return;
            }

            // 日期变更，先刷旧缓冲区，切换新文件
            var rollDate = GetRollDate();
            if (rollDate != _currentRollDate)
            {
                FlushLocked();

                _currentRollDate = rollDate;
                _currentFilePath = ResolvePath(_currentRollDate);
            }

            try
            {
                using var memoryStream = new MemoryStream();
                using var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);

                streamWriter.Write(text);
                streamWriter.Flush();
                memoryStream.Flush();

                var payloadSpan = memoryStream.GetBuffer().AsSpan(0, (int)memoryStream.Length);
                var totalLen = payloadSpan.Length + 1;

                if (_batchBuffer.WrittenCount + totalLen > _options.BatchBufferSize)
                {
                    FlushLocked();
                }

                payloadSpan.CopyTo(_batchBuffer.GetSpan(payloadSpan.Length));

                _batchBuffer.Advance(payloadSpan.Length);
                _newline.AsSpan().CopyTo(_batchBuffer.GetSpan(1));
                _batchBuffer.Advance(1);

                TryAutoFlushLocked();

                _metrics.IncrementWriteSuccess();
            }
            catch (Exception exception)
            {
                _logger.LogAdvanceWarning($"write file error:{exception.Message}");
                _metrics.IncrementSerializeFailed();
            }
        }
    }

    /// <summary>
    /// 追加原始UTF8字节流，不自动追加换行
    /// 适合已经外部格式化好的文本、二进制片段
    /// 线程安全
    /// </summary>
    /// <param name="utf8Bytes">原始utf8字节</param>
    public void AppendRaw(ReadOnlySpan<byte> utf8Bytes)
    {
        lock (_sync)
        {
            if (_disposed)
            {
                return;
            }

            if (_options.EnableDiskPressureProtection && IsDiskLow())
            {
                _metrics.IncrementDiscardByDiskPressure();

                return;
            }

            var rollDate = GetRollDate();

            if (rollDate != _currentRollDate)
            {
                FlushLocked();

                _currentRollDate = rollDate;
                _currentFilePath = ResolvePath(_currentRollDate);
            }

            if (_batchBuffer.WrittenCount + utf8Bytes.Length > _options.BatchBufferSize)
            {
                FlushLocked();
            }

            utf8Bytes.CopyTo(_batchBuffer.GetSpan(_batchBuffer.WrittenCount));

            _batchBuffer.Advance(utf8Bytes.Length);

            TryAutoFlushLocked();
        }
    }

    /// <summary>
    /// 强制同步刷盘，阻塞直到落地完成
    /// 场景：程序优雅关闭、单元测试、需要立即可读文件
    /// </summary>
    public void FlushNow()
    {
        lock (_sync)
        {
            if (_disposed)
            {
                return;
            }

            FlushLocked();
        }
    }

    /// <summary>
    /// 检查磁盘剩余空间是否低于阈值
    /// </summary>
    private bool IsDiskLow()
    {
        try
        {
            var dir = Path.GetDirectoryName(_currentFilePath);

            if (string.IsNullOrEmpty(dir))
            {
                return false;
            }

            var drive = new DriveInfo(dir);

            return drive.AvailableFreeSpace < _options.DiskMinFreeBytes;
        }
        catch
        {
            // 获取磁盘信息失败，保守放行，不丢弃记录
            return false;
        }
    }

    /// <summary>
    /// 判断是否满足自动刷盘条件（缓冲区满 / 超时）
    /// </summary>
    private void TryAutoFlushLocked()
    {
        var now = DateTimeOffset.UtcNow;

        if (_batchBuffer.WrittenCount >= _options.BatchBufferSize || now - _lastFlush > _options.FlushInterval)
        {
            FlushLocked();
        }
    }

    /// <summary>
    /// 同步刷盘（持有锁内调用）
    /// </summary>
    private void FlushLocked()
    {
        if (_batchBuffer.WrittenCount == 0)
        {
            _lastFlush = DateTimeOffset.UtcNow;

            return;
        }

        var span = _batchBuffer.WrittenSpan;
        var dir = Path.GetDirectoryName(_currentFilePath)!;

        Directory.CreateDirectory(dir);

        try
        {
            if (_options.UseTempFileRename)
            {
                // 原子模式：先写临时文件，完成后rename覆盖
                var tmpPath = $"{_currentFilePath}.tmp";

                using var fileStream = new FileStream(
                    tmpPath,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite,
                    _options.StreamBufferSize,
                    FileOptions.SequentialScan
                );

                fileStream.Write(span);
                fileStream.Flush();

                File.Move(
                    tmpPath,
                    _currentFilePath,
                    true
                );
            }
            else
            {
                // 直接追加模式
                using var fileStream = new FileStream(
                    _currentFilePath,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite,
                    _options.StreamBufferSize,
                    FileOptions.SequentialScan
                );

                fileStream.Write(span);
                fileStream.Flush();
            }

            _batchBuffer.Clear();
            _metrics.IncrementFlush();

            TryRunMaintenance();
        }
        catch
        {
            _metrics.IncrementFlushFailed();
            throw;
        }
        finally
        {
            // 无论成功失败，都更新上次尝试时间，防止定时器风暴重试
            _lastFlush = DateTimeOffset.UtcNow;
        }
    }

    /// <summary>
    /// 目录自维护：淘汰旧文件
    /// </summary>
    /// <remarks>
    /// <para>触发时机：每次刷盘成功；受ScanCooldown冷却控制，不会频繁扫描</para>
    /// <para>判定规则：总容量超限 OR 文件存活超时，且保留最少MinKeepFileCount个文件</para>
    /// <para>排序：按文件创建时间升序，优先删最早的</para>
    /// <para>所有内部异常全部捕获，只打点，绝不阻塞写入链路</para>
    /// </remarks>
    private void TryRunMaintenance()
    {
        // 0 = 关闭自动清理
        if (_options.MaxDirectorySizeBytes <= 0)
        {
            return;
        }

        var now = DateTimeOffset.UtcNow;

        lock (_sync)
        {
            if (now - _lastScanTime < _options.ScanCooldown)
            {
                return;
            }

            _lastScanTime = now;
        }

        try
        {
            var dir = Path.GetDirectoryName(_currentFilePath)!;

            if (!Directory.Exists(dir))
            {
                return;
            }

            var files = new DirectoryInfo(dir)
                .GetFiles("*", SearchOption.TopDirectoryOnly)
                .OrderBy(f => f.CreationTimeUtc)
                .ToList();

            if (files.Count <= _options.MinKeepFileCount)
            {
                return;
            }

            var totalSize = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (files.Count <= _options.MinKeepFileCount)
                {
                    break;
                }

                var overSize = totalSize > _options.MaxDirectorySizeBytes;
                var overAge = _options.MaxFileAge.HasValue
                              && DateTimeOffset.UtcNow - file.CreationTimeUtc > _options.MaxFileAge.Value;

                if (!overSize && !overAge)
                {
                    break;
                }

                try
                {
                    file.Delete();

                    _metrics.IncrementCleanDeleteFile();

                    totalSize -= file.Length;
                }
                catch
                {
                    // 单个文件删除失败，继续尝试下一个
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            _metrics.IncrementCleanError();
        }
        catch (IOException)
        {
            _metrics.IncrementCleanError();
        }
        catch
        {
            _metrics.IncrementCleanError();
        }
    }

    /// <summary>
    /// PeriodicTimer后台巡检循环
    /// </summary>
    private async Task RunTimerLoopAsync()
    {
        try
        {
            while (await _autoFlushTimer.WaitForNextTickAsync())
            {
                lock (_sync)
                {
                    if (_disposed)
                    {
                        return;
                    }

                    TryAutoFlushLocked();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 正常停止
        }
        catch (Exception ex)
        {
            _logger.LogAdvanceWarning("Auto flush timer loop exception: {Message}", ex.Message);
        }
    }

    /// <summary>
    /// 释放资源：停止定时巡检、强制刷盘，写入剩余缓冲区数据
    /// 必须在父模块生命周期结束时调用
    /// </summary>
    public void Dispose()
    {
        // 停止定时器，不再产生新tick
        _autoFlushTimer.Dispose();

        // 等待定时器循环正常退出
        if (_timerLoopTask != null)
        {
            _timerLoopTask.Wait();
            _timerLoopTask = null;
        }

        lock (_sync)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            FlushLocked();
        }
    }
}