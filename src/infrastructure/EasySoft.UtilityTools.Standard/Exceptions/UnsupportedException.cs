using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Standard.Exceptions;

public class UnsupportedException : System.Exception
{
    /// <summary>
    /// UnsupportedException
    /// </summary>
    /// <param name="message"></param>
    public UnsupportedException(string message = "") : base("Unsupported:{0}".FormatValue(message))
    {
    }
}