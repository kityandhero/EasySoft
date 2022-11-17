namespace EasySoft.Core.PermissionVerification.Attributes;

/// <summary>
/// GuidTagAttribute
/// </summary>
public class GuidTagAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// GuidTag
    /// </summary>
    public string GuidTag { get; }

    #endregion Properties

    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    public GuidTagAttribute() : this("")
    {
    }

    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    /// <param name="guidTag"></param>
    public GuidTagAttribute(string guidTag)
    {
        GuidTag = guidTag;
    }
}