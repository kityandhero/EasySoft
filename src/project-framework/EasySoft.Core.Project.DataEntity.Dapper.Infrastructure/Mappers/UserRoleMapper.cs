using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class UserRoleMapper : BaseMapper<UserRole>
{
    public UserRoleMapper(
        IMapperChannel mapperChannel,
        bool logSql = true
    ) : base(mapperChannel, logSql)
    {
    }

    public UserRoleMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }
}