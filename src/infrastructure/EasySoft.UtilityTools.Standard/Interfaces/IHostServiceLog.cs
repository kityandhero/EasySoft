namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// IHostServiceLog
/// </summary>
public interface IHostServiceLog
{
    /// <summary>
    /// 访问名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 变动类型
    /// </summary>
    public int ChangeType { get; set; }

    /// <summary>
    /// 服务识别码
    /// </summary>
    public string ServiceChannel { get; set; }
}