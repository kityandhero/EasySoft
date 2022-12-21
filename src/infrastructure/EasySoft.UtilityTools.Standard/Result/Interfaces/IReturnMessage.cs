namespace EasySoft.UtilityTools.Standard.Result.Interfaces;

/// <summary>
/// return message
/// </summary>
public interface IReturnMessage
{
    /// <summary>
    /// Success
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Extra
    /// </summary>
    public object Extra { get; set; }
}