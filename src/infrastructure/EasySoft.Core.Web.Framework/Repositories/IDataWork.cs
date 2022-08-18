using System.Data.Common;

namespace EasySoft.Core.Web.Framework.Repositories;

public interface IDataWork
{
    DbTransaction BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();
}