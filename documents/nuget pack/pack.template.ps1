# Get-ChildItem ./publish -Name *.nupkg

Get-ChildItem ./publish -Name *.nupkg | ForEach-Object -Process{
   dotnet nuget push ./publish/$_ -k <key> -s https://api.nuget.org/v3/index.json

   }  