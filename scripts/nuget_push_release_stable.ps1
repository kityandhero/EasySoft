./release_stable.ps1

# 更改变量的可见性, 运行脚本时多加上一个点. 和一个空格
. ../private/nuget_key.ps1

echo nugetkey:$myStr

Get-ChildItem ../publish -Name *.nupkg | ForEach-Object -Process{
   dotnet nuget push ../publish/$_ -k $myStr -s https://api.nuget.org/v3/index.json --skip-duplicate
}  