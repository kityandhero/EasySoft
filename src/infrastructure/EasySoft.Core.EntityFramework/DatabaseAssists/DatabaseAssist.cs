namespace EasySoft.Core.EntityFramework.DatabaseAssists;

/// <summary>
/// DatabaseAssist
/// </summary>
public static class DatabaseAssist
{
    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="context"></param>
    public static void Initialize(DbContext context)
    {
        context.Database.EnsureCreated();
    }
}