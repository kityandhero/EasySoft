namespace EasySoft.Core.Data.Contexts.Interfaces;

/// <summary>
/// IDataContext
/// </summary>
public interface IDataContext
{
    void BeforeSave();

    void AfterSave();

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}