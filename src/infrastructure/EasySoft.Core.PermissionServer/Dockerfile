FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["infrastructure/EasySoft.Core.PermissionServer/EasySoft.Core.PermissionServer.csproj", "EasySoft.Core.PermissionServer/"]
RUN dotnet restore "infrastructure/EasySoft.Core.PermissionServer/EasySoft.Core.PermissionServer.csproj"
COPY . .
WORKDIR "/src/EasySoft.Core.PermissionServer"
RUN dotnet build "EasySoft.Core.PermissionServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasySoft.Core.PermissionServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySoft.Core.PermissionServer.dll"]
