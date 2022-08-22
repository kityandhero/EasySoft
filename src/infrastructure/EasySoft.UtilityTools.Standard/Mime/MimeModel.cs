using System;
using System.Collections.Generic;

namespace EasySoft.UtilityTools.Standard.Mime
{
    /// <summary>
    /// ContentType定义
    /// </summary>
    [Serializable]
    public class MimeModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 名称
        /// </summary>
        public string Extension { get; set; } = null!;

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; } = null!;

        /// <summary>
        /// 内容类型别名
        /// </summary>
        public List<string> Alias { get; set; } = null!;
    }
}