using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods
{
    public static class ReturnCodeExtension
    {
        public static ReturnMessage ToMessage(this ReturnCode returnCode)
        {
            return new ReturnMessage(returnCode);
        }

        public static ReturnMessage ToMessage(this ReturnCode returnCode, string message)
        {
            return new ReturnMessage(returnCode).ToMessage(message);
        }

        public static ReturnMessage ToMessage(this ReturnCode returnCode, bool success)
        {
            return new ReturnMessage(returnCode).ToMessage(success);
        }

        public static ReturnMessage ToMessage(this ReturnCode returnCode, int code)
        {
            return new ReturnMessage(returnCode).ToMessage(code);
        }
    }
}