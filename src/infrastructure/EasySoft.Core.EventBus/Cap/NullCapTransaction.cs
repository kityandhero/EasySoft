namespace EasySoft.Core.EventBus.Cap;

/// <summary>
/// 空事务实现：配合NullCapPublisher使用
/// 业务代码调用BeginTransaction拿到此对象，Commit/Rollback全部空操作，不会产生数据库事务与消息表操作
/// 实现 ICapTransaction，同时兼容 IDisposable + IAsyncDisposable
/// </summary>
public class NullCapTransaction : ICapTransaction
{
    /// <summary>
    /// 关联的底层数据库事务对象，空事务返回null
    /// </summary>
    public object DbTransaction { get; set; }

    /// <summary>
    /// 是否自动提交事务，空事务固定false
    /// </summary>
    public bool AutoCommit { get; set; } = false;

    /// <summary>
    /// 同步提交事务：空操作
    /// </summary>
    public void Commit()
    {
        // 空实现
    }

    /// <summary>
    /// 异步提交事务：空操作
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 同步回滚事务：空操作
    /// </summary>
    public void Rollback()
    {
        // 空实现
    }

    /// <summary>
    /// 异步回滚事务：空操作
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>已完成任务</returns>
    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 同步释放资源：空操作
    /// </summary>
    public void Dispose()
    {
        // 空实现
    }

    /// <summary>
    /// 异步释放资源：空操作
    /// </summary>
    /// <returns>已完成ValueTask</returns>
    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}