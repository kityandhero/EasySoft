FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/ClassLibrary1EasySoft.Simple.Gateway/ClassLibrary1EasySoft.Simple.Gateway.csproj", "ClassLibrary1EasySoft.Simple.Gateway/"]
RUN dotnet restore "test/ClassLibrary1EasySoft.Simple.Gateway/ClassLibrary1EasySoft.Simple.Gateway.csproj"
COPY . .
WORKDIR "/src/ClassLibrary1EasySoft.Simple.Gateway"
RUN dotnet build "ClassLibrary1EasySoft.Simple.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClassLibrary1EasySoft.Simple.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClassLibrary1EasySoft.Simple.Gateway.dll"]
