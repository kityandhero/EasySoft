using System;
using System.ComponentModel;

namespace EasySoft.UtilityTools.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class RegularAttribute : DescriptionAttribute
    {
        public RegularAttribute(string regular) : base(regular)
        {
        }
    }
}