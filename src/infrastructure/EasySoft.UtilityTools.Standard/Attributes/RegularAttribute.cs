using System;
using System.ComponentModel;

namespace EasySoft.UtilityTools.Standard.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class RegularAttribute : DescriptionAttribute
    {
        public RegularAttribute(string regular) : base(regular)
        {
        }
    }
}