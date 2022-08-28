using System.Data;
using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Dapper.Interfaces;
using Microsoft.Data.SqlClient;
using StackExchange.Profiling.Data;

namespace EasySoft.Core.Dapper.Common
{
    public class MapperTransaction : IMapperTransaction
    {
        private readonly ProfiledDbConnection _dbConnection;

        private readonly IDbTransaction _dbTransaction;

        private readonly IMapperChannel _mapperChannel;

        public MapperTransaction(IMapperChannel mapperChannel)
        {
            _mapperChannel = new MapperChannel(
                mapperChannel.GetChannel(),
                mapperChannel.GetConnectionString(),
                mapperChannel.GetRelationDatabaseType()
            );

            switch (_mapperChannel.GetRelationDatabaseType())
            {
                case RelationDatabaseType.SqlServer:
                    _dbConnection = new ProfiledDbConnection(
                        new SqlConnection(_mapperChannel.GetConnectionString()),
                        StackExchange.Profiling.MiniProfiler.Current
                    );
                    break;

                default:
                    throw new Exception("未知的关系型数据库");
            }

            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public ProfiledDbConnection GetDbConnection()
        {
            return _dbConnection;
        }

        public IDbTransaction GetTransaction()
        {
            return _dbTransaction;
        }

        public IMapperChannel GetMapperChannel()
        {
            return _mapperChannel;
        }

        public RelationDatabaseType GetRelationDatabaseType()
        {
            return _mapperChannel.GetRelationDatabaseType();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
            _dbTransaction.Dispose();
        }
    }
}