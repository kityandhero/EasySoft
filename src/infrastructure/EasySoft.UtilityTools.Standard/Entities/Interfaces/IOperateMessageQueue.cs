namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

public interface IOperateMessageQueue
{
    /// <summary>
    /// 触发渠道码
    /// </summary>
    [Description("触发渠道码")]
    public int TriggerChannel { get; set; }
}