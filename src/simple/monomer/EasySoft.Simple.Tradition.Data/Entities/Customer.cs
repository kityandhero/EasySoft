using System.ComponentModel;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

/// <summary>
/// 顾客信息
/// </summary>
[Description("顾客信息")]
public class Customer : BaseEntity
{
    /// <summary>
    /// 用户标识
    /// </summary>
    [Description("用户标识")]
    public long UserId { get; set; }

    public Customer()
    {
        UserId = 0;
    }
}