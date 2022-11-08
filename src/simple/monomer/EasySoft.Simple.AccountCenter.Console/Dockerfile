FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["simple/monomer/EasySoft.Simple.AccountCenter.Console/EasySoft.Simple.AccountCenter.Console.csproj", "EasySoft.Simple.AccountCenter.Console/"]
RUN dotnet restore "simple/monomer/EasySoft.Simple.AccountCenter.Console/EasySoft.Simple.AccountCenter.Console.csproj"
COPY . .
WORKDIR "/src/EasySoft.Simple.AccountCenter.Console"
RUN dotnet build "EasySoft.Simple.AccountCenter.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasySoft.Simple.AccountCenter.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySoft.Simple.AccountCenter.Console.dll"]
