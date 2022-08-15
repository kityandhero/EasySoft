using System.ComponentModel;

namespace EasySoft.UtilityTools.Attributes
{
    public class CustomColumnMapperAttribute : DescriptionAttribute
    {
        public string Name { get; }

        public CustomColumnMapperAttribute() : this("")
        {
        }

        public CustomColumnMapperAttribute(string name, string description = "") : base(description)
        {
            Name = name;
        }
    }
}