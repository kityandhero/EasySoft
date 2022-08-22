using System.ComponentModel;

namespace EasySoft.UtilityTools.Standard.Attributes
{
    public class ReturnCodeSuccessAttribute : DescriptionAttribute
    {
        public bool Success { get; }

        public ReturnCodeSuccessAttribute() : this(false)
        {
        }

        public ReturnCodeSuccessAttribute(bool success, string description = "") : base(description)
        {
            Success = success;
        }
    }
}