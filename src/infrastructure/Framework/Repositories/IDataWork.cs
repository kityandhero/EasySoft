using System.Data.Common;

namespace Framework.Repositories;

public interface IDataWork
{
    DbTransaction BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();
}