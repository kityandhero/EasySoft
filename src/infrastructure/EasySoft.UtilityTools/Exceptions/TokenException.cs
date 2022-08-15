using System;

namespace EasySoft.UtilityTools.Exceptions
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