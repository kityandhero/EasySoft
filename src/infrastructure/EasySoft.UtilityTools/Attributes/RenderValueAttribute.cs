using System;
using System.ComponentModel;

namespace EasySoft.UtilityTools.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class RenderValueAttribute : DescriptionAttribute
    {
        public RenderValueAttribute(string renderValue) : base(renderValue)
        {
        }

        public RenderValueAttribute() : base("")
        {
        }
    }
}