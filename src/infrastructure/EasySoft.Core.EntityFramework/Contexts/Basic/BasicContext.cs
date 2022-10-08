﻿namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class BasicContext : DbContext, IDataContext
{
    protected BasicContext(
        DbContextOptions options
    ) : base(options)
    {
        Database.AutoTransactionsEnabled = false;
    }

    /// <summary>
    ///     保存前预处理
    /// </summary>
    public virtual void BeforeSave()
    {
    }

    /// <summary>
    ///     保存后触发
    /// </summary>
    public virtual void AfterSave()
    {
    }

    public override int SaveChanges()
    {
        var changeCount = GetChangeCount();

        //没有自动开启事务的情况下,保证主从表插入，主从表更新开启事务。
        var isManualTransaction = false;

        if (!Database.AutoTransactionsEnabled && Database.CurrentTransaction is null && changeCount > 1)
        {
            isManualTransaction = true;
            Database.AutoTransactionsEnabled = true;
        }

        var result = base.SaveChanges();

        //如果手工开启了自动事务，用完后关闭。
        if (isManualTransaction) Database.AutoTransactionsEnabled = false;

        return result;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changeCount = GetChangeCount();

        //没有自动开启事务的情况下,保证主从表插入，主从表更新开启事务。
        var isManualTransaction = false;

        if (!Database.AutoTransactionsEnabled && Database.CurrentTransaction is null && changeCount > 1)
        {
            isManualTransaction = true;
            Database.AutoTransactionsEnabled = true;
        }

        var result = base.SaveChangesAsync(cancellationToken);

        //如果手工开启了自动事务，用完后关闭。
        if (isManualTransaction) Database.AutoTransactionsEnabled = false;

        return result;
    }

    /// <summary>
    ///     获取数据变更量
    /// </summary>
    /// <returns></returns>
    protected virtual int GetChangeCount()
    {
        return ChangeTracker.Entries().Count();
    }
}