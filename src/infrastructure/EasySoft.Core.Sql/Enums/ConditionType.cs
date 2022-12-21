namespace EasySoft.Core.Sql.Enums;

/// <summary>
/// 筛选条件
/// </summary>
public enum ConditionType
{
    /// <summary>
    /// 等于
    /// </summary>
    [Description("等于")]
    Eq,

    /// <summary>
    /// 不等于
    /// </summary>
    [Description("不等于")]
    Ne,

    /// <summary>
    /// 大于
    /// </summary>
    [Description("大于")]
    Gt,

    /// <summary>
    /// 大于等于
    /// </summary>
    [Description("大于等于")]
    Gte,

    /// <summary>
    /// 小于
    /// </summary>
    [Description("小于")]
    Lt,

    /// <summary>
    /// 小于等于
    /// </summary>
    [Description("小于等于")]
    Lte,

    /// <summary>
    /// 包括指定的所有值,可以指定不同类型的条件和值
    /// </summary>
    [Description("包括指定的所有值,可以指定不同类型的条件和值")]
    In,

    /// <summary>
    /// 返回与数组中所有条件都不匹配
    /// </summary>
    [Description("返回与数组中所有条件都不匹配")]
    NotIn,

    /// <summary>
    /// 模糊查询
    /// </summary>
    [Description("模糊查询")]
    LikeBefore,

    /// <summary>
    /// 模糊查询
    /// </summary>
    [Description("模糊查询")]
    LikeAfter,

    /// <summary>
    /// 模糊查询
    /// </summary>
    [Description("模糊查询")]
    LikeAny,

    /// <summary>
    /// is null
    /// </summary>
    [Description("Is Null")]
    IsNull
}