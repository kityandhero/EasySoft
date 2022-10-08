namespace EasySoft.Core.EntityFramework.SqlServer.Transactions;

public class UnitOfWork<TContext> : BasicUnitOfWork<TContext>
    where TContext : BaseContext
{
    private readonly ICapPublisher? _publisher;

    public UnitOfWork(TContext context, ICapPublisher? publisher = null) : base(context)
    {
        _publisher = publisher;
    }

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