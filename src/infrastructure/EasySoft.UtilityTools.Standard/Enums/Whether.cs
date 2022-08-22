using System.ComponentModel;

namespace EasySoft.UtilityTools.Standard.Enums
{
    /// <summary>
    /// Whether
    /// </summary>
    public enum Whether
    {
        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        No = 0,

        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        Yes = 1,
    }

    public static class WhetherExtensionMethods
    {
        public static bool ToBool(this Whether whether)
        {
            return whether == Whether.Yes;
        }

        public static int ToInt(this Whether whether)
        {
            return (int)whether;
        }
    }
}