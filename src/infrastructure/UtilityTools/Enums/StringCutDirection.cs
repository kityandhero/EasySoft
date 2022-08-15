using System.ComponentModel;

namespace UtilityTools.Enums
{
    public enum StringCutDirection
    {
        /// <summary>
        /// 从左边开始计数
        /// </summary>
        [Description("从左边开始计数")]
        FromLeft = 1,

        /// <summary>
        /// 从左边开始计数
        /// </summary>
        [Description("从右边开始计数")]
        FromRight = 2,
    }
}