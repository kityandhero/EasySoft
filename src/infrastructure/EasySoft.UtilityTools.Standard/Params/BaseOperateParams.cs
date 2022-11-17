namespace EasySoft.UtilityTools.Standard.Params;

/// <summary>
/// BaseOperateParams
/// </summary>
public abstract class BaseOperateParams : IOperateParams
{
    /// <summary>
    /// OperatorId
    /// </summary>
    public long OperatorId { get; set; }

    /// <summary>
    /// OperatorName
    /// </summary>
    public string OperatorName { get; set; }

    /// <summary>
    /// BaseOperateParams
    /// </summary>
    protected BaseOperateParams()
    {
        OperatorName = "";
    }
}