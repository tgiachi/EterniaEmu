using CommandLine;

namespace EterniaEmu.Server.Options;

public class EterniaServerOptions
{
    [Option('d', "debug", Required = false, Default = false, HelpText = "Enable debug mode")]
    public bool Debug { get; set; }

    [Option('c', "config", Required = false, Default = "config.json", HelpText = "Path to the configuration file")]
    public string ConfigFile { get; set; }

    [Option('u', "ultima", Required = false, Default = ".", HelpText = "Path to the Ultima Online client")]
    public string UltimaPath { get; set; }
}
