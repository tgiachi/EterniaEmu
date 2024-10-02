FROM mcr.microsoft.com/dotnet/runtime:latest AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/", "src/"]
RUN dotnet restore "src/EterniaEmu.Server/EterniaEmu.Server.csproj"
COPY . .
WORKDIR "/src/src/EterniaEmu.Server"
RUN dotnet build "EterniaEmu.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EterniaEmu.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
EXPOSE 2593
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EterniaEmu.Server.dll"]
