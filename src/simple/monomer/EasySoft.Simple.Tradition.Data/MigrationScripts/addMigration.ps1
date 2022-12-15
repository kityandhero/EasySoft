$time = Get-Date -format "yyyy_MM_dd_ss_mm_HH_fff"

cd ..

dotnet ef migrations add --context EasySoft.Simple.Tradition.Data.Contexts.SqlServerDataContext --configuration Debug $time

cd ./MigrationScripts