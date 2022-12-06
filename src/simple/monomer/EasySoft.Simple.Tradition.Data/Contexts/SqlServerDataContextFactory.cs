using EasySoft.Core.Config.ConfigAssist;
using Microsoft.EntityFrameworkCore.Design;

namespace EasySoft.Simple.Tradition.Data.Contexts;

// 使用 migrations 从类库创建是需要提供 Factory 用以能够实例化 Context
// https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
// public class SqlServerDataContextFactory : IDesignTimeDbContextFactory<SqlServerDataContext>
// {
//     public SqlServerDataContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<SqlServerDataContext>();
//         optionsBuilder.UseSqlServer(DatabaseConfigAssist.GetMainConnection());
//
//         return new SqlServerDataContext(optionsBuilder.Options);
//     }
// }