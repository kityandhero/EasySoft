namespace EasySoft.Core.MultiTenant;

public class Tenant : ITenant
{
    private object? _id;

    protected Tenant()
    {
        _id = default;
    }

    protected Tenant(object id)
    {
        _id = id;
    }

    public ITenant SetTenantId(object id)
    {
        _id = id;

        return this;
    }

    public object? GetTenantId()
    {
        return _id;
    }
}