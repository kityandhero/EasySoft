using Microsoft.EntityFrameworkCore;

namespace EasySoft.Core.Mvc.Framework.DatabaseAssists;

public static class DatabaseAssist
{
    public static void Initialize(DbContext context)
    {
        context.Database.EnsureCreated();
    }
}