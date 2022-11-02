using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

namespace EasySoft.Core.Domain.Base.Entities.Implementations;

/// <summary>
/// 基础聚合根, (Fluent API 配置的优先级高于数据注释属性, 同时配置可考虑兼容 EntityFrameWork Core 之外的 ORM, 若无兼容性需求, 选其一即可, 推荐 Fluent API, 数据注释属性设计上容易引起 DDD Domain 定义混淆, 可考虑继承接口并在接口上定义数据注释属性)
/// </summary>
public abstract class BaseAggregateOperatorRoot : BaseAggregateRoot, IOperate
{
    /// <summary>
    /// 创建人
    /// </summary>
    public virtual long CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTime CreateTime { get; set; }

    /// <summary>
    /// 最后更新人
    /// </summary>
    public virtual long ModifyBy { get; set; }

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public virtual DateTime ModifyTime { get; set; }
}