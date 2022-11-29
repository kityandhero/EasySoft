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

    /// <summary>
    /// 权限组
    /// </summary>
    public int Group { get; }

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
        Group = 0;
    }

    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    /// <param name="guidTag"></param>
    /// <param name="group">权限组</param>
    public GuidTagAttribute(string guidTag, int group)
    {
        GuidTag = guidTag;
        Group = group;
    }
}