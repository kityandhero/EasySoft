using Microsoft.EntityFrameworkCore;

namespace Framework.DatabaseAssists;

public static class DatabaseAssist
{
    public static void Initialize(DbContext context)
    {
        context.Database.EnsureCreated();
    }
}