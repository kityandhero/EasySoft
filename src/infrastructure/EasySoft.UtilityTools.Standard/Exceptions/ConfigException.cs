﻿namespace EasySoft.UtilityTools.Standard.Exceptions
{
    public class ConfigException : System.Exception
    {
        public ConfigException() : this("")
        {
        }

        public ConfigException(string message) : base(message)
        {
        }
    }
}