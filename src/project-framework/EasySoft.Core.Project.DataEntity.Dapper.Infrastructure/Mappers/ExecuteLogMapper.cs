using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class ExecuteLogMapper : BaseMapper<ExecuteLog>
{
    public ExecuteLogMapper(
        IMapperChannel databaseChannel,
        bool logSql = true
    ) : base(databaseChannel, logSql)
    {
    }

    public ExecuteLogMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }
}