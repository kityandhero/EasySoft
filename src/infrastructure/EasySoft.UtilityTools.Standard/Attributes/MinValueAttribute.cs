namespace EasySoft.UtilityTools.Standard.Attributes;

/// <summary>
/// MinValueAttribute
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class MinValueAttribute : Attribute
{
    /// <summary>
    /// Value
    /// </summary>
    public object Value { get; } = null!;

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public MinValueAttribute(Type type, string value)
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
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(byte value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(short value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(int value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(long value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(float value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(double value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(bool value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(string value)
    {
        Value = value;
    }

    /// <summary>
    /// MinValueAttribute
    /// </summary>
    /// <param name="value"></param>
    public MinValueAttribute(object value)
    {
        Value = value;
    }
}