using System.Data;
using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Sql.Enums;
using StackExchange.Profiling.Data;

namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IMapperChannel
    {
        string GetChannel();

        string GetConnectionString();

        RelationDatabaseType GetRelationDatabaseType();

        IDbConnection OpenConnection();

        IMapperTransaction CreateMapperTransaction();
    }
}