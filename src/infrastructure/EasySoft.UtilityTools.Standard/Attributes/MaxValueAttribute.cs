namespace EasySoft.UtilityTools.Standard.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class MaxValueAttribute : Attribute
{
    public object Value { get; } = null!;

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

    public MaxValueAttribute(byte value)
    {
        Value = value;
    }

    public MaxValueAttribute(short value)
    {
        Value = value;
    }

    public MaxValueAttribute(int value)
    {
        Value = value;
    }

    public MaxValueAttribute(long value)
    {
        Value = value;
    }

    public MaxValueAttribute(float value)
    {
        Value = value;
    }

    public MaxValueAttribute(double value)
    {
        Value = value;
    }

    public MaxValueAttribute(bool value)
    {
        Value = value;
    }

    public MaxValueAttribute(string value)
    {
        Value = value;
    }

    public MaxValueAttribute(object value)
    {
        Value = value;
    }
}