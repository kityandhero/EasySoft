namespace EasySoft.UtilityTools.Standard.Params;

/// <summary>
/// IOperateParams
/// </summary>
public interface IOperateParams : IApiParams
{
    /// <summary>
    /// OperatorId
    /// </summary>
    public long OperatorId { get; set; }

    /// <summary>
    /// OperatorName
    /// </summary>
    public string OperatorName { get; set; }
}