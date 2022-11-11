namespace EasySoft.Core.EventBus.Cap;

public class CapSubscriber : IEventSubscriber, ICapSubscribe
{
    protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(
        params Expression<Func<TEntity, object>>[] expressions
    )
    {
        return expressions;
    }
}