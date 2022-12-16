cd ..

dotnet ef database update --verbose --context EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Contexts.DataMigrationContext --configuration Debug

cd ./MigrationScripts