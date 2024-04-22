using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class GeneralDiscourseMapper : BaseMapper<GeneralDiscourse>
{
    public GeneralDiscourseMapper(
        IMapperChannel mapperChannel,
        bool logSql = true
    ) : base(mapperChannel, logSql)
    {
    }

    public GeneralDiscourseMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }
}