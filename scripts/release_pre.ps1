./clean.ps1

dotnet build ../src/EasySoft.sln -c release /p:PublicRelease=false /p:PackageLicenseExpression=MIT -o ../publish