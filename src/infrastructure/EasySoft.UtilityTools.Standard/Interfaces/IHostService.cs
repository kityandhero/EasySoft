namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 驻留服务
/// </summary>
public interface IHostService
{
    /// <summary>
    /// 访问名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 服务识别码
    /// </summary>
    public string ServiceChannel { get; set; }
}