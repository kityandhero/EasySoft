using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class OptionPoolMapper : BaseMapper<OptionPool>
{
    public OptionPoolMapper(
        IMapperChannel mapperChannel,
        bool logSql = true
    ) : base(mapperChannel, logSql)
    {
    }

    public OptionPoolMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }
}