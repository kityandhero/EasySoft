using System.Linq.Expressions;
using EasySoft.UtilityTools.Standard;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Entities.Common.Bases;

public abstract class AbstractFunctionEntity<T> : BaseEntity<T> where T : BaseEntity<T>
{
    #region Properties

    [AdvanceColumnInformation(
        "数据渠道",
        "用于标记数据的来源, 进行数据隔离,比如来自于哪个系统"
    )]
    [AdvanceColumnMapper("channel")]
    [AdvanceColumnLength(200)]
    [AdvanceColumnNational]
    public string Channel { get; set; } = UtilityTools.Standard.Models.Channel.Unknown.ToValue();

    [AdvanceColumnInformation("状态码")]
    [AdvanceColumnMapper("status")]
    public int Status { get; set; }

    [AdvanceColumnInformation("创建人标识")]
    [AdvanceColumnMapper("create_by")]
    public long CreateBy { get; set; }

    [AdvanceColumnInformation("创建时间")]
    [AdvanceColumnMapper("create_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime CreateTime { get; set; }

    [AdvanceColumnInformation("最后更新人标识")]
    [AdvanceColumnMapper("modify_by")]
    public long ModifyBy { get; set; }

    [AdvanceColumnInformation("最后更新时间")]
    [AdvanceColumnMapper("modify_time")]
    [AdvanceColumnDefaultValue(ConstCollection.DatabaseDefaultDateTime)]
    public DateTime ModifyTime { get; set; }

    #endregion

    public sealed override Expression<Func<T, object>> GetPrimaryKeyLambda()
    {
        return o => o.Id;
    }

    public string GetIdString()
    {
        return Id.ToString();
    }

    public string GetId()
    {
        return Id.ToString();
    }

    public string GetIdentificationName()
    {
        return ReflectionAssist.GetPropertyName(() => Id);
    }
}