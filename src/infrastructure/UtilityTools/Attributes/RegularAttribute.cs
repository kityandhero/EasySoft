using System;
using System.ComponentModel;

namespace UtilityTools.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class RegularAttribute : DescriptionAttribute
    {
        public RegularAttribute(string regular) : base(regular)
        {
        }
    }
}