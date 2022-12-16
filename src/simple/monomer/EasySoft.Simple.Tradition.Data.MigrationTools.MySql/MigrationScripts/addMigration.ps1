$time = Get-Date -format "TyyyyMMddssmmHHfff"

cd ..

dotnet ef migrations add --verbose --context EasySoft.Simple.Tradition.Data.MigrationTools.MySql.Contexts.DataMigrationContext --configuration Debug $time

cd ./MigrationScripts