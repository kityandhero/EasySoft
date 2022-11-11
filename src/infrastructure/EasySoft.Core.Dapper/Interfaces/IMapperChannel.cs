namespace EasySoft.Core.Dapper.Interfaces;

public interface IMapperChannel
{
    string GetChannel();

    string GetConnectionString();

    RelationDatabaseType GetRelationDatabaseType();

    IDbConnection OpenConnection();

    IMapperTransaction CreateMapperTransaction();
}