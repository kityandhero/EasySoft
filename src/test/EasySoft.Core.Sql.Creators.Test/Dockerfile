FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["test/EasySoft.Core.Sql.Creators.Test/EasySoft.Core.Sql.Creators.Test.csproj", "test/EasySoft.Core.Sql.Creators.Test/"]
RUN dotnet restore "test/EasySoft.Core.Sql.Creators.Test/EasySoft.Core.Sql.Creators.Test.csproj"
COPY . .
WORKDIR "/src/test/EasySoft.Core.Sql.Creators.Test"
RUN dotnet build "EasySoft.Core.Sql.Creators.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EasySoft.Core.Sql.Creators.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySoft.Core.Sql.Creators.Test.dll"]
