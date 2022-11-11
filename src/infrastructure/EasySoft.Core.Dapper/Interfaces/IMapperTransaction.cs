namespace EasySoft.Core.Dapper.Interfaces;

public interface IMapperTransaction : IDisposable
{
    ProfiledDbConnection GetDbConnection();

    IDbTransaction GetTransaction();

    IMapperChannel GetMapperChannel();

    RelationDatabaseType GetRelationDatabaseType();

    void Commit();

    void Rollback();
}