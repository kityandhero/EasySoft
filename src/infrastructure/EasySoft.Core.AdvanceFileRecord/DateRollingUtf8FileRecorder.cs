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
    /// 换行符字节，Write 序列化对象后自动追加
    /// </summary>
    private readonly byte[] _newline = { (byte)'\n' };

    /// <summary>实例锁，保护缓冲区、日期滚动、刷盘、状态变更</summary>
    private readonly Lock _sync = new();

    /// <summary>当前正在写入的文件所属日期</summary>
    private DateTime _currentRollDate;

    /// <summary>当前目标文件完整路径</summary>
    private string _currentFilePath;

    /// <summary>上一次刷盘的UTC时间</summary>
    private DateTimeOffset _lastFlush;

    /// <summary>上一次目录维护扫描UTC时间，用于冷却控制</summary>
    private DateTimeOffset _lastScanTime = DateTimeOffset.MinValue;

    /// <summary>释放标记</summary>
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
    }

    /// <summary>获取当前滚动判定日期（UTC/本地时间由配置控制）</summary>
    private DateTime GetRollDate()
    {
        return _options.UseLocalTimeForRolling ? DateTime.Now.Date : DateTime.UtcNow.Date;
    }

    /// <summary>根据日期解析路径模板</summary>
    private string ResolvePath(DateTime date)
    {
        return _options.PathTemplate.Replace("{Date:yyyyMMdd}", date.ToString("yyyyMMdd"));
    }

    /// <summary>
    /// 写入对象
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
    /// 写入对象
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

                // 拷贝序列化结果 + 换行符到批量缓冲区
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

                // 序列化异常，计数，不中断整体写入流程
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
    /// 强制异步刷盘，IO异步等待
    /// 场景：异步优雅退出、异步工作流
    /// </summary>
    public Task FlushNowAsync()
    {
        lock (_sync)
        {
            return _disposed ? Task.CompletedTask : FlushAsyncLocked();
        }
    }

    /// <summary>检查磁盘剩余空间是否低于阈值</summary>
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

    /// <summary>判断是否满足自动刷盘条件（缓冲区满 / 超时）</summary>
    private void TryAutoFlushLocked()
    {
        var now = DateTimeOffset.UtcNow;

        if (_batchBuffer.WrittenCount >= _options.BatchBufferSize || now - _lastFlush > _options.FlushInterval)
        {
            FlushLocked();
        }
    }

    /// <summary>同步刷盘（持有锁内调用）</summary>
    private void FlushLocked()
    {
        if (_batchBuffer.WrittenCount == 0)
        {
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
            _lastFlush = DateTimeOffset.UtcNow;
            _metrics.IncrementFlush();

            // 刷盘成功后触发目录自维护（带冷却，不会每次都扫描）
            TryRunMaintenance();
        }
        catch
        {
            _metrics.IncrementFlushFailed();

            throw; // 刷盘IO异常向上抛出，上层可以感知数据落地失败
        }
    }

    /// <summary>异步刷盘（持有锁内调用）</summary>
    private async Task FlushAsyncLocked()
    {
        if (_batchBuffer.WrittenCount == 0)
        {
            return;
        }

        var readOnlyMemory = _batchBuffer.WrittenMemory;
        var dir = Path.GetDirectoryName(_currentFilePath)!;

        Directory.CreateDirectory(dir);

        try
        {
            if (_options.UseTempFileRename)
            {
                var tmpPath = $"{_currentFilePath}.tmp";

                await using var fileStream = new FileStream(
                    tmpPath,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite,
                    _options.StreamBufferSize,
                    FileOptions.Asynchronous | FileOptions.SequentialScan
                );

                await fileStream.WriteAsync(readOnlyMemory);
                await fileStream.FlushAsync();

                File.Move(
                    tmpPath,
                    _currentFilePath,
                    true
                );
            }
            else
            {
                await using var fileStream = new FileStream(
                    _currentFilePath,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite,
                    _options.StreamBufferSize,
                    FileOptions.Asynchronous | FileOptions.SequentialScan
                );

                await fileStream.WriteAsync(readOnlyMemory);
                await fileStream.FlushAsync();
            }

            lock (_sync)
            {
                _batchBuffer.Clear();
                _lastFlush = DateTimeOffset.UtcNow;
            }

            _metrics.IncrementFlush();

            TryRunMaintenance();
        }
        catch
        {
            _metrics.IncrementFlushFailed();

            throw;
        }
    }

    /// <summary>
    /// 目录自维护：淘汰旧文件
    /// 触发时机：每次刷盘成功；受ScanCooldown冷却控制，不会频繁扫描
    /// 判定规则：总容量超限 OR 文件存活超时，且保留最少MinKeepFileCount个文件
    /// 排序：按文件创建时间升序，优先删最早的
    /// 所有内部异常全部捕获，只打点，绝不阻塞写入链路
    /// </summary>
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
                var overAge = _options.MaxFileAge.HasValue &&
                              DateTimeOffset.UtcNow - file.CreationTimeUtc > _options.MaxFileAge.Value;

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
    /// 释放资源：强制刷盘，写入剩余缓冲区数据
    /// 必须在父模块生命周期结束时调用
    /// </summary>
    public void Dispose()
    {
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