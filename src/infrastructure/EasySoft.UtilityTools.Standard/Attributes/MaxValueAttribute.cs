namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// MaxValueAttribute
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class MaxValueAttribute : Attribute
{
    public object Value { get; } = null!;

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public MaxValueAttribute(Type type, string value)
    {
        try
        {
            Value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value)!;
        }
        catch
        {
            // ignored
        }
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(byte value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(short value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(int value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(long value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(float value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(double value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// MaxValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MaxValueAttribute(object value)
    {
        Value = value;
    }
}