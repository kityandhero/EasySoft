using EasySoft.Core.EntityFramework.Contexts.Basic;

namespace EasySoft.Core.EntityFramework.SqlServer.Contexts;

public class DataContext : BasicContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
}