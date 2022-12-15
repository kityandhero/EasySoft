namespace EasySoft.Core.EntityFramework.ValueGenerators;

/// <summary>
/// IdentifierGenerator
/// </summary>
public abstract class IdentifierGenerator : ValueGenerator
{
    /// <summary>
    /// GeneratesTemporaryValues
    /// </summary>
    public override bool GeneratesTemporaryValues => false;

    /// <summary>
    /// NextValue
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    protected override object NextValue(EntityEntry entry)
    {
        return IdentifierAssist.Create();
    }
}