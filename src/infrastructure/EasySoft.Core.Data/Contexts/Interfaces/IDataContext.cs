namespace EasySoft.Core.Data.Contexts.Interfaces;

/// <summary>
/// IDataContext
/// </summary>
public interface IDataContext
{
    /// <summary>
    /// BeforeSave
    /// </summary>
    void BeforeSave();

    /// <summary>
    /// AfterSave
    /// </summary>
    void AfterSave();

    /// <summary>
    /// SaveChanges
    /// </summary>
    /// <returns></returns>
    int SaveChanges();

    /// <summary>
    /// SaveChangesAsync
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}