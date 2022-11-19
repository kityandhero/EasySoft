FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/EasySoft.Simple.Gateway/EasySoft.Simple.Gateway.csproj", "EasySoft.Simple.Gateway/"]
RUN dotnet restore "test/EasySoft.Simple.Gateway/EasySoft.Simple.Gateway.csproj"
COPY . .
WORKDIR "/src/EasySoft.Simple.Gateway"
RUN dotnet build "EasySoft.Simple.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasySoft.Simple.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySoft.Simple.Gateway.dll"]
