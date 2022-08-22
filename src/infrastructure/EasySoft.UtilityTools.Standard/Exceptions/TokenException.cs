using System;

namespace EasySoft.UtilityTools.Standard.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException() : this("")
        {
        }

        public TokenException(string message) : base(message)
        {
        }
    }
}