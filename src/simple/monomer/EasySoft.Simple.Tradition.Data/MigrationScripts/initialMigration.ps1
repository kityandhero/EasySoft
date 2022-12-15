cd ..

dotnet ef migrations add --context EasySoft.Simple.Tradition.Data.Contexts.SqlServerDataContext --configuration Debug InitialCreate

cd ./MigrationScripts