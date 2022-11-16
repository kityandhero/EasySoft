namespace EasySoft.UtilityTools.Standard.Exceptions;

public class IgnoreException : Exception
{
    public IgnoreException() : this("")
    {
    }

    public IgnoreException(string message) : base(message)
    {
    }
}