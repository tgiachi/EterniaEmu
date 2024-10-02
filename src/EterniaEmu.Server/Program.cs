using CommandLine;
using EterniaEmu.Core.Config.Sections;
using EterniaEmu.Core.Extensions;
using EterniaEmu.Server.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();


EterniaServerOptions options = null;

Parser.Default.ParseArguments<EterniaServerOptions>(args).WithParsed(o => options = o);

var builder = Host.CreateApplicationBuilder(args);

builder.Logging
    .ClearProviders()
    .AddSerilog();


builder.RegisterConfig<ServerConfig>("Server");


builder.Configuration
    .AddJsonFile(options.ConfigFile, optional: true)
    .AddJsonFile($"config.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();


// var server = new EterniaTcpServer(System.Net.IPAddress.Any, 2593);
//
//
// server.AddPacketType(PacketTypeEnum.LoginSeed, typeof(LoginSeedPacket));
//
// server.Start();
//
//
// while (true)
// {
//     Thread.Sleep(1000);
// }


var app = builder.Build();


await app.RunAsync();
