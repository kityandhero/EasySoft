namespace EasySoft.UtilityTools.Standard;

/// <summary>
/// 常用正则表达式
/// </summary>
public class PatternCollection
{
    /// <summary>
    /// 中英文人称、昵称、单位名称等
    /// </summary>
    public const string Name = @"^[\u4E00-\u9FA5A-Za-z0-9\. ]*$";

    /// <summary>
    /// 密码
    /// </summary>
    public const string Password = @"^[\s\S]*$";

    /// <summary>
    /// 搜索关键字
    /// </summary>
    public const string Keyword = @"^[\u4E00-\u9FA5A-Za-z0-9]$";

    /// <summary>
    /// 手机号码
    /// </summary>
    public const string Phone = @"^1[3-9]\d{9}$";

    /// <summary>
    /// 正整数
    /// </summary>
    public const string Number = @"^[1-9]\d*$";

    /// <summary>
    /// Email地址
    /// </summary>
    public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    /// <summary>
    /// Http协议的Url地址
    /// </summary>
    public const string HttpUrl = @"^http://\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    /// <summary>
    /// Https协议的Url地址
    /// </summary>
    public const string HttpsUrl = @"^https://\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    /// <summary>
    /// 中英文地址
    /// </summary>
    public const string Address = @"^[\u4E00-\u9FA5，\,（）\(\)\[\]\<\>A-Za-z0-9\.\- ]*$";

    /// <summary>
    /// 任意数字
    /// </summary>
    public const string Digits = @"^\d*$";

    /// <summary>
    /// 警告：请注意XSS攻击和SQL注入漏洞！
    /// </summary>
    public const string Text = @"^[\s\S]*$";

    /// <summary>
    /// 数字逗号分隔列表
    /// </summary>
    public const string NumberList = @"^\d+(,\d+)*$";

    /// <summary>
    /// 大小写字母和数字
    /// </summary>
    public const string AlphabetDigits = @"^[A-Z0-9a-z]*$";

    /// <summary>
    /// 大小写字母
    /// </summary>
    public const string Alphabets = @"^[A-Za-z]*$";

    /// <summary>
    /// 车牌号
    /// </summary>
    public const string CarNumber = @"^豫[A-Z][0-9A-Za-z]{5}$";

    /// <summary>
    /// 时间，格式：10:30
    /// </summary>
    public const string Time = @"^([0-9]|[1][0-9]|[2][0-3]):([0-5][0-9])$";

    /// <summary>
    /// 移动设备
    /// </summary>
    public const string MobileDevice = @"AppleWebKit.*Mobile.*$";

    /// <summary>
    /// 新浪微博Agent
    /// </summary>
    public const string WeiBoAgent = @"Weibo";

    /// <summary>
    /// 微信Agent
    /// </summary>
    public const string WeiXinAgent = @"MicroMessenger";

    /// <summary>
    /// QQ Agent
    /// </summary>
    public const string QQAgent = @"QQ";

    /// <summary>
    /// Android Agent
    /// </summary>
    public const string AndroidAgent = @"Android";

    /// <summary>
    /// Ios Agent
    /// </summary>
    public const string IOSAgent = @"iPhone|iPad|iPod";
}