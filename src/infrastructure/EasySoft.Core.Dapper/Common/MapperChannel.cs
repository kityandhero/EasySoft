using System.Data;
using EasySoft.Core.Dapper.Assist;
using EasySoft.Core.Dapper.Enums;
using EasySoft.Core.Dapper.Interfaces;

namespace EasySoft.Core.Dapper.Common
{
    [Serializable]
    public class MapperChannel : IMapperChannel
    {
        private readonly string _channel;

        private readonly string _connectionString;

        private RelationDatabaseType _relationDatabaseType;

        public MapperChannel(string connectionString, RelationDatabaseType relationDatabaseType) : this(
            "",
            connectionString,
            relationDatabaseType
        )
        {
        }

        public MapperChannel(string channel, string connectionString, RelationDatabaseType relationDatabaseType)
        {
            _channel = channel;
            _connectionString = connectionString;
            _relationDatabaseType = relationDatabaseType;
        }

        public string GetChannel()
        {
            return _channel;
        }

        public string GetConnectionString()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new Exception("无效的连接字符串");
            }

            return _connectionString;
        }

        public RelationDatabaseType GetRelationDatabaseType()
        {
            return _relationDatabaseType;
        }

        public IDbConnection OpenConnection()
        {
            return SqlAssist.CreateConnection(_connectionString, _relationDatabaseType);
        }

        public IMapperTransaction CreateMapperTransaction()
        {
            return new MapperTransaction(this);
        }
    }
}