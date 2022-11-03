using System.Reflection;
using DotNetCore.CAP.Processor;
using EasySoft.Core.EntityFramework.EntityTypeConfigures;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.EntityFramework.Contexts.Basic;

public abstract class BasicContext : DbContext, IDataContext
{
    protected BasicContext(
        DbContextOptions options
    ) : base(options)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Database.AutoTransactionsEnabled = false;
    }

    protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        OnEntityTypeConfig(modelBuilder);

        OnAdvanceModelCreating(modelBuilder);

        OnSeedCreating(modelBuilder);
    }

    protected virtual void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
    }

    /// <summary>
    /// 配置实体
    /// </summary>
    /// <param name="modelBuilder"></param>
    private void OnEntityTypeConfig(ModelBuilder modelBuilder)
    {
        var assemblies = GetEntityTypeConfigureAssemblies();

        foreach (var assembly in assemblies) ConfigEntityType(modelBuilder, assembly);
    }

    protected virtual List<Assembly> GetEntityTypeConfigureAssemblies()
    {
        return new List<Assembly>();
    }

    protected virtual void OnSeedCreating(ModelBuilder modelBuilder)
    {
    }

    private static void ConfigEntityType(ModelBuilder modelBuilder, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(m => m.FullName != null &&
                        typeof(IAdvanceEntityTypeConfiguration).IsAssignableFrom(m) &&
                        !m.IsAbstract).ToList();

        foreach (var type in types)
        {
            type.Create();

            dynamic configurationInstance = Activator.CreateInstance(type) ?? throw new InvalidOperationException();

            modelBuilder.ApplyConfiguration(configurationInstance);
        }
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