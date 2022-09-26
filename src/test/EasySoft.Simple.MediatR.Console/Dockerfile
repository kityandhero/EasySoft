FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/EasySoft.Simple.MediatR.Console/EasySoft.Simple.MediatR.Console.csproj", "EasySoft.Simple.MediatR.Console/"]
RUN dotnet restore "test/EasySoft.Simple.MediatR.Console/EasySoft.Simple.MediatR.Console.csproj"
COPY . .
WORKDIR "/src/EasySoft.Simple.MediatR.Console"
RUN dotnet build "EasySoft.Simple.MediatR.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasySoft.Simple.MediatR.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySoft.Simple.MediatR.Console.dll"]
