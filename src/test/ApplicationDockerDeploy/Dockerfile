FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

COPY . /app

#RUN dotnet restore
#RUN dotnet publish -o /out -c Release

EXPOSE 80

ENTRYPOINT ["dotnet", "/app/ApplicationDockerDeploy.dll"]