FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["simple/monomer/EasySoft.Simple.Tradition.ManagementApplication/EasySoft.Simple.Tradition.ManagementApplication.csproj", "EasySoft.Simple.Tradition.ManagementApplication/"]
RUN dotnet restore "simple/monomer/EasySoft.Simple.Tradition.ManagementApplication/EasySoft.Simple.Tradition.ManagementApplication.csproj"
COPY . .
WORKDIR "/src/EasySoft.Simple.Tradition.ManagementApplication"
RUN dotnet build "EasySoft.Simple.Tradition.ManagementApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasySoft.Simple.Tradition.ManagementApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySoft.Simple.Tradition.ManagementApplication.dll"]
