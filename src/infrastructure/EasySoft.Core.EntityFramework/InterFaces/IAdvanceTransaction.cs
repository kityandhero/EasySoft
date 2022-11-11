namespace EasySoft.Core.EntityFramework.InterFaces;

public interface IAdvanceTransaction
{
    IDbContextTransaction GetCurrentTransaction();

    bool HasActiveTransaction { get; }

    Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus);

    Task CommitAsync(IDbContextTransaction transaction);

    void Rollback();
}