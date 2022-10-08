namespace EasySoft.Core.Data.Contexts;

public interface IDataContext
{
    void BeforeSave();

    void AfterSave();

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}