namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// IStatus
/// </summary>
public interface IStatus
{
    /// <summary>
    /// 状态码
    /// </summary>
    [Description("状态码")]
    int Status { get; set; }
}