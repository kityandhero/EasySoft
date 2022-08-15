using System;
using System.ComponentModel;

namespace EasySoft.UtilityTools.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class MaxValueAttribute: Attribute
    {
      
        public object Value { get; } = null!;

        public MaxValueAttribute(Type type, string value)
        {
            try
            {
                this.Value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value)!;
            }
            catch
            {
                // ignored
            }
        }

        public MaxValueAttribute(byte value) => this.Value = value;

        public MaxValueAttribute(short value) => this.Value = value;

        public MaxValueAttribute(int value) => this.Value = value;

        public MaxValueAttribute(long value) => this.Value = value;

        public MaxValueAttribute(float value) => this.Value = value;

        public MaxValueAttribute(double value) => this.Value = value;

        public MaxValueAttribute(bool value) => this.Value = value;

        public MaxValueAttribute(string value) => this.Value = value;

        public MaxValueAttribute(object value) => this.Value = value;
    }
}