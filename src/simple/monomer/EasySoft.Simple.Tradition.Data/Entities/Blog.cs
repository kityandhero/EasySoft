using System.ComponentModel;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

/// <summary>
/// 博客
/// </summary>
[Description("博客")]
public class Blog : BaseEntity
{
    /// <summary>
    /// 名称"
    /// </summary>
    [Description("名称")]
    public string Name { get; set; }

    /// <summary>
    /// 笔名
    /// </summary>
    [Description("笔名")]
    public string Pseudonym { get; set; }

    /// <summary>
    /// 座右铭
    /// </summary>
    [Description("座右铭")]
    public string Motto { get; set; }

    [Description("用户标识")]
    public long UserId { get; set; }

    public ICollection<Post>? Posts { get; set; }

    public Blog()
    {
        Name = string.Empty;
        Pseudonym = string.Empty;
        Motto = string.Empty;
    }
}