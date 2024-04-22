using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class SmsLogMapper : BaseMapper<SmsLog>
{
    public SmsLogMapper(
        IMapperChannel mapperChannel,
        bool logSql = true
    ) : base(mapperChannel, logSql)
    {
    }

    public SmsLogMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }
}