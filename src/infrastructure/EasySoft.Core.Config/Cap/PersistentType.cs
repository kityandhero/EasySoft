using System.ComponentModel;

namespace EasySoft.Core.Config.Cap;

/// <summary>
/// 持久化方式
/// </summary>
[Description("持久化方式")]
public enum PersistentType
{
    /// <summary>
    /// SqlServer
    /// </summary>
    [Description("SqlServer")]
    SqlServer = 100,

    /// <summary>
    /// SqlServer
    /// </summary>
    [Description("MySql")]
    MySql = 200,

    /// <summary>
    /// SqlServer
    /// </summary>
    [Description("PostgreSql")]
    PostgreSql = 300,

    /// <summary>
    /// SqlServer
    /// </summary>
    [Description("MongoDB")]
    MongoDB = 400,

    /// <summary>
    /// SqlServer
    /// </summary>
    [Description("ImMemory")]
    ImMemory = 500,

    /// <summary>
    /// SqlServer
    /// </summary>
    [Description("Sqlite")]
    Sqlite = 600
}