# Get-ChildItem ./publish -Name *.nupkg

Get-ChildItem ./publish -Name *.nupkg | ForEach-Object -Process{
   dotnet nuget push ./publish/$_ -k oy2lsggxsw6zycg3hxmqdfees2sf3hmeddzlloweql377y -s https://api.nuget.org/v3/index.json

   }  