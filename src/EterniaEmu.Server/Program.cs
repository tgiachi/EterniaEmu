using CommandLine;
using ConfigurationSubstitution;
using EterniaEmu.Core.Config;
using EterniaEmu.Core.Extensions;
using EterniaEmu.Core.Utils;
using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Extensions;
using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Server;
using EterniaEmu.Network.Packets;
using EterniaEmu.Server.Handlers;
using EterniaEmu.Server.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;


EterniaServerOptions options = null;

Parser.Default.ParseArguments<EterniaServerOptions>(args).WithParsed(o => options = o);

var builder = Host.CreateApplicationBuilder(args);


builder.Logging
    .ClearProviders()
    .AddSerilog();


builder
    .RegisterConfig<ServerConfig>("Server")
    .RegisterConfig<LogConfig>("Log");

var config = builder.Configuration
    .AddJsonFile(options.ConfigFile)
    .AddEnvironmentVariables()
    .EnableSubstitutions()
    .Build();

var logConfig = config.GetSection("Log").Get<LogConfig>() ?? new LogConfig();


if (logConfig.LogToFile)
{
    var logPath = Path.Join(EnvUtils.GetRootDirectory(), logConfig.LogPath);

    if (!Directory.Exists(logPath))
    {
        Directory.CreateDirectory(logPath);
    }
}


var logConfiguration = new LoggerConfiguration()
    .Enrich
    .FromLogContext();

logConfiguration = logConfiguration.MinimumLevel.Is(logConfig.LogLevel.ToSerilogLevel());

if (logConfig.LogToFile)
{
    logConfiguration = logConfiguration.WriteTo.File(
        Path.Join(EnvUtils.GetRootDirectory(), logConfig.LogPath, logConfig.LogFile),
        rollingInterval: RollingInterval.Day
    );
}

if (logConfig.LogToConsole)
{
    logConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Code);
}


Log.Logger = logConfiguration.CreateLogger();


Log.Logger.Debug("Scanning for network packets");

AssemblyUtils.GetAttribute<NetworkPacketAttribute>()
    .ForEach(
        n =>
        {
            Log.Logger.Debug("Registering network packet {Packet}", n.Name);
            builder.Services.RegisterNetworkPacket(n);
        }
    );


builder.Services.AddSingleton<IEterniaEmuTcpServer, EterniaEmuTcpServer>();


var app = builder.Build();


await using var scope = app.Services.CreateAsyncScope();

var server = scope.ServiceProvider.GetRequiredService<IEterniaEmuTcpServer>();

server.AddPacketListener<LoginSeedPacket>(new LoginListener());

server.Start();
await app.RunAsync();
