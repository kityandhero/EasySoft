using System.ComponentModel;

namespace EasySoft.Core.Sql.Enums
{
    /// <summary>
    /// 筛选条件
    /// </summary>
    public enum ConditionType
    {
        [Description("等于")]
        Eq,

        [Description("不等于")]
        Ne,

        [Description("大于")]
        Gt,

        [Description("大于等于")]
        Gte,

        [Description("小于")]
        Lt,

        [Description("小于等于")]
        Lte,

        [Description("包括指定的所有值,可以指定不同类型的条件和值")]
        In,

        [Description("返回与数组中所有条件都不匹配")]
        NotIn,

        [Description("模糊查询")]
        LikeBefore,

        [Description("模糊查询")]
        LikeAfter,

        [Description("模糊查询")]
        LikeAny,

        [Description("Is Null")]
        IsNull,
    }
}