﻿using CommandLine;
using ConfigurationSubstitution;
using EterniaEmu.Core.Config;
using EterniaEmu.Core.Extensions;
using EterniaEmu.Core.Utils;
using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Extensions;
using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Server;
using EterniaEmu.Server.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;


EterniaServerOptions options = null;

Parser.Default.ParseArguments<EterniaServerOptions>(args).WithParsed(o => options = o);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen)
    .CreateLogger();

var builder = Host.CreateApplicationBuilder(args);

builder.Logging
    .ClearProviders()
    .AddSerilog();


builder.RegisterConfig<ServerConfig>("Server");


builder.Configuration
    .AddJsonFile(options.ConfigFile, optional: true)
    .AddEnvironmentVariables()
    .EnableSubstitutions();


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

scope.ServiceProvider.GetRequiredService<IEterniaEmuTcpServer>();

await app.RunAsync();
