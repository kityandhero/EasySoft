namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 用户核心信息
/// </summary>
public interface IUserPure
{
    /// <summary>
    /// 缓存键
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    string Avatar { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    string RealName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    string Nickname { get; set; }

    /// <summary>
    /// 印章
    /// </summary>
    string Signet { get; set; }
}