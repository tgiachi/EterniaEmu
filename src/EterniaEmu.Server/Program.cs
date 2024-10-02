using CommandLine;
using ConfigurationSubstitution;
using EterniaEmu.Core.Config.Sections;
using EterniaEmu.Core.Extensions;
using EterniaEmu.Server.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    .AddEnvironmentVariables()
    .EnableSubstitutions();


var app = builder.Build();


await app.RunAsync();
