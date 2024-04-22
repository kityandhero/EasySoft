using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class ApplicationMapper : BaseMapper<Application>
{
    public ApplicationMapper(
        IMapperChannel databaseChannel,
        bool logSql = true
    ) : base(databaseChannel, logSql)
    {
    }

    public ApplicationMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }
}