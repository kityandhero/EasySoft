rmdir ../publish -recurse

dotnet clean ../src/EasySoft.sln

dotnet clean ../src/EasySoft.sln -c release