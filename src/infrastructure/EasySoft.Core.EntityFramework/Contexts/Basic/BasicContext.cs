using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class BasicContext : DbContext, IDataContext
{
    private readonly IEntityConfigure _entityConfigure;

    protected BasicContext(
        DbContextOptions options,
        IEntityConfigure entityConfigure
    ) : base(options)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Database.AutoTransactionsEnabled = false;

        _entityConfigure = entityConfigure;
    }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        OnEntityTypeConfig(modelBuilder);

        OnAdvanceModelCreating(modelBuilder);

        OnSeedCreating(modelBuilder);
    }

    // protected virtual void BuildTenantQueryFilter(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<MultitenantContact>()
    //         .HasQueryFilter(mt => mt.Tenant == _tenant);
    // }

    protected virtual void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
    }

    /// <summary>
    /// 配置实体
    /// </summary>
    /// <param name="modelBuilder"></param>
    private void OnEntityTypeConfig(ModelBuilder modelBuilder)
    {
        // var assemblies = GetEntityTypeConfigureAssemblies();
        //
        // assemblies.Add(typeof(EventTracker).Assembly);
        //
        // foreach (var assembly in assemblies) ConfigEntityType(modelBuilder, assembly);

        _entityConfigure.OnModelCreating(modelBuilder);
    }

    protected virtual void OnSeedCreating(ModelBuilder modelBuilder)
    {
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