namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// advance table
/// </summary>
public class AdvanceTableMapperAttribute : DescriptionAttribute
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// AdvanceTableMapperAttribute
    /// </summary>
    public AdvanceTableMapperAttribute() : this("")
    {
    }

    /// <summary>
    /// AdvanceTableMapperAttribute
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public AdvanceTableMapperAttribute(string name, string description = "") : base(description)
    {
        Name = name;
    }
}