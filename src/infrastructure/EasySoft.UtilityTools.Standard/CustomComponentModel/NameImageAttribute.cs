namespace EasySoft.UtilityTools.Standard.CustomComponentModel;

/// <summary>
/// NameImageAttribute
/// </summary>
public class NameImageAttribute : DescriptionAttribute
{
    /// <summary>
    /// Image
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// NameImageAttribute
    /// </summary>
    /// <param name="name"></param>
    /// <param name="image"></param>
    public NameImageAttribute(string name, string image) : base(name)
    {
        Image = image;
    }
}