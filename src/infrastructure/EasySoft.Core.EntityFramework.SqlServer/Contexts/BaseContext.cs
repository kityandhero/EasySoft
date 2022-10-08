using EasySoft.Core.EntityFramework.Contexts.Basic;

namespace EasySoft.Core.EntityFramework.SqlServer.Contexts;

public class BaseContext : BasicContext
{
    public BaseContext(DbContextOptions options) : base(options)
    {
    }
}