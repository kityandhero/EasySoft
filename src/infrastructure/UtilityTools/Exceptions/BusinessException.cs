﻿namespace UtilityTools.Exceptions
{
    public class BusinessException : System.Exception
    {
        public BusinessException() : this("")
        {
        }

        public BusinessException(string message) : base(message)
        {
        }
    }
}