using System.Data.Common;

namespace EasySoft.Core.Mvc.Framework.Repositories;

public interface IDataWork
{
    DbTransaction BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();
}