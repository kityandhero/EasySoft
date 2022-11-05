namespace EasySoft.Simple.Tradition.Data.Contexts;

// 使用 migrations 从类库创建是需要提供 Factory 用以能够实例化 Context
// https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
// public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
// {
//     public DataContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
//         optionsBuilder.UseSqlServer(DatabaseConfigAssist.GetMainConnection());
//
//         return new DataContext(optionsBuilder.Options);
//     }
// }