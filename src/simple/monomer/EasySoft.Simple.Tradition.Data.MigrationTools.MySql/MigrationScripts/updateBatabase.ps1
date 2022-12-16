cd ..

dotnet ef database update --verbose --context EasySoft.Simple.Tradition.Data.MigrationTools.MySql.Contexts.DataMigrationContext --configuration Debug

cd ./MigrationScripts