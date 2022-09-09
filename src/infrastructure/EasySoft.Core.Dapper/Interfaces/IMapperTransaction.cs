using System.Data;
using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Sql.Enums;
using StackExchange.Profiling.Data;

namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IMapperTransaction : IDisposable
    {
        ProfiledDbConnection GetDbConnection();

        IDbTransaction GetTransaction();

        IMapperChannel GetMapperChannel();

        RelationDatabaseType GetRelationDatabaseType();

        void Commit();

        void Rollback();
    }
}