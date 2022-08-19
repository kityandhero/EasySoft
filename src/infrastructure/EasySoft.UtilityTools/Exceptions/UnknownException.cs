using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.UtilityTools.Exceptions
{
    /// <summary>
    /// 未知的错误
    /// </summary>
    public class UnknownException : System.Exception
    {
        /// <summary>
        /// UnknownException
        /// </summary>
        /// <param name="message"></param>
        public UnknownException(string message = "") : base("未知的错误{0}".FormatValue(message))
        {
        }
    }
}