namespace EasySoft.Core.EntityFramework.SqlServer.Transactions;

/// <summary>
/// UnitOfWork
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class UnitOfWork<TContext> : BasicUnitOfWork<TContext>
    where TContext : SqlServerContext
{
    private readonly ICapPublisher? _publisher;

    /// <summary>
    /// UnitOfWork
    /// </summary>
    /// <param name="context"></param>
    /// <param name="publisher"></param>
    public UnitOfWork(TContext context, ICapPublisher? publisher = null) : base(context)
    {
        _publisher = publisher;
    }

    /// <summary>
    /// GetDbContextTransaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="distributed"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    protected override IDbContextTransaction GetDbContextTransaction(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        bool distributed = false
    )
    {
        if (!distributed)
            return AdvanceContext.Database.BeginTransaction(isolationLevel);

        if (_publisher is null)
            throw new ArgumentException("CapPublisher is null");

        return AdvanceContext.Database.BeginTransaction(_publisher);
    }
}