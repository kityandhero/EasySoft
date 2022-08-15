using System;

namespace UtilityTools.Exceptions
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