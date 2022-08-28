using EasySoft.Core.Dapper.Enums;
using StackExchange.Profiling.Data;

namespace EasySoft.Core.Dapper.Interfaces
{
    public interface IMapperChannel
    {
        string GetChannel();

        string GetConnectionString();

        RelationDatabaseType GetRelationDatabaseType();

        ProfiledDbConnection OpenConnection();

        IMapperTransaction CreateMapperTransaction();
    }
}