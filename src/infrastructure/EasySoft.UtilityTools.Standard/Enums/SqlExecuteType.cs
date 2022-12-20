namespace EasySoft.UtilityTools.Standard.Enums;

/// <summary>
/// sql execute type
/// </summary>
public enum SqlExecuteType
{
    /// <summary>
    /// Unknown
    /// </summary>
    [Description("Unknown")]
    Unknown = 0,

    /// <summary>
    /// Custom
    /// </summary>
    [Description("Custom")]
    Custom = 100,

    /// <summary>
    /// EntityFramework
    /// </summary>
    [Description("EntityFramework")]
    EntityFramework = 200,

    /// <summary> 
    /// MiniProfiler
    /// </summary>
    [Description("MiniProfiler")]
    MiniProfiler = 300
}