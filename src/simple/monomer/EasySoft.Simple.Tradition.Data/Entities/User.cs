using System.ComponentModel;
using EasySoft.Simple.Tradition.Data.Entities.Bases;

namespace EasySoft.Simple.Tradition.Data.Entities;

/// <summary>
/// 基础账户
/// </summary>
[Description("基础账户")]
public class User : BaseEntity
{
    /// <summary>
    /// 别名
    /// </summary>
    [Description("别名")]
    public string Alias { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [Description("真实姓名")]
    public string RealName { get; set; }

    /// <summary>
    /// 登录名
    /// </summary>
    [Description("登录名")]
    public string LoginName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Description("密码")]
    public string Password { get; set; }

    public User()
    {
        Alias = "";
        RealName = "";
        LoginName = "";
        Password = "";
    }
}