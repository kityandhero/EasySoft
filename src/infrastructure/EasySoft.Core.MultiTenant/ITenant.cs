namespace EasySoft.Core.MultiTenant;

public interface ITenant
{
    public ITenant SetTenantId(object id);

    public object? GetTenantId();
}