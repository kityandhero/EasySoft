using System;
using System.ComponentModel;

namespace UtilityTools.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class MinValueAttribute : Attribute
    {
        public object Value { get; } = null!;

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

        public MinValueAttribute(byte value) => Value = value;

        public MinValueAttribute(short value) => Value = value;

        public MinValueAttribute(int value) => Value = value;

        public MinValueAttribute(long value) => Value = value;

        public MinValueAttribute(float value) => Value = value;

        public MinValueAttribute(double value) => Value = value;

        public MinValueAttribute(bool value) => Value = value;

        public MinValueAttribute(string value) => Value = value;

        public MinValueAttribute(object value) => Value = value;
    }
}