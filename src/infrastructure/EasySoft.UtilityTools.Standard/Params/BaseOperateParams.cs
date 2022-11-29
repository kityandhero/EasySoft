namespace EasySoft.UtilityTools.Standard.Params;

/// <summary>
/// BaseOperateParams
/// </summary>
public abstract class BaseOperateParams : IOperateParams
{
    /// <inheritdoc />
    public long OperatorId { get; set; }

    /// <inheritdoc />
    public string OperatorName { get; set; }

    /// <summary>
    /// BaseOperateParams
    /// </summary>
    protected BaseOperateParams()
    {
        OperatorName = "";
    }
}