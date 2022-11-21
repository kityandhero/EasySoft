namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// ReturnCodeSuccessAttribute
/// </summary>
public class ReturnCodeSuccessAttribute : DescriptionAttribute
{
    /// <summary>
    /// Success
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// ReturnCodeSuccessAttribute
    /// </summary>
    public ReturnCodeSuccessAttribute() : this(false)
    {
    }

    /// <summary>
    /// ReturnCodeSuccessAttribute
    /// </summary>
    /// <param name="success"></param>
    /// <param name="description"></param>
    public ReturnCodeSuccessAttribute(bool success, string description = "") : base(description)
    {
        Success = success;
    }
}