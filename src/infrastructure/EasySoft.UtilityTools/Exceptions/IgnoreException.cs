using System;

namespace EasySoft.UtilityTools.Exceptions
{
    public class IgnoreException : Exception
    {
        public IgnoreException() : this("")
        {
        }

        public IgnoreException(string message) : base(message)
        {
        }
    }
}