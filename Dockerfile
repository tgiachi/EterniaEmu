FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EterniaEmu.Server/EterniaEmu.Server.csproj", "src/EterniaEmu.Server/"]
RUN dotnet restore "src/EterniaEmu.Server/EterniaEmu.Server.csproj"
COPY . .
WORKDIR "/src/src/EterniaEmu.Server"
RUN dotnet build "EterniaEmu.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EterniaEmu.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EterniaEmu.Server.dll"]
