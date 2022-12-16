cd ..

dotnet ef migrations add --verbose --context EasySoft.Simple.Tradition.Data.MigrationTools.SqlServer.Contexts.DataMigrationContext --configuration Debug InitialCreate

cd ./MigrationScripts