using EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities;

namespace EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Mappers;

public class DepartmentMapper : BaseMapper<Department>
{
    public DepartmentMapper(
        IMapperChannel mapperChannel,
        bool logSql = true
    ) : base(mapperChannel, logSql)
    {
    }

    public DepartmentMapper(
        IMapperTransaction mapperTransaction,
        bool logSql = true
    ) : base(mapperTransaction, logSql)
    {
    }

    protected override Department PreSave(Department model)
    {
        model.ModifyTime = DateTime.Now;

        return model;
    }
}