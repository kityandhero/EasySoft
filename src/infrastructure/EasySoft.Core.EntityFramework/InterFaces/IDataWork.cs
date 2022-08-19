using System.Data.Common;

namespace EasySoft.Core.EntityFramework.InterFaces;

public interface IDataWork
{
    DbTransaction BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();
}