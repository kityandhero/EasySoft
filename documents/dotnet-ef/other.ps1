dotnet ef migrations add NewColum --新增migrations
dotnet ef database update--跟新数据库
dotnet ef migrations add Addrs--新增一个migrations
dotnet ef database update
dotnet ef datebase update NewColum--根据newcolum跟新数据库
dotnet ef migrations remove--删除最新未使用的migrations