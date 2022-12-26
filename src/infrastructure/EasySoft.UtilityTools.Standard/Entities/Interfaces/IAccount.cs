namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// 登录信息
/// </summary>
public interface IAccount
{
    /// <summary>
    /// 登录账户名
    /// </summary>
    string UserName { get; set; }

    /// <summary>
    /// 登录用密码
    /// </summary>
    string Password { get; set; }
}