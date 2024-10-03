using EterniaEmu.Core.Types;

namespace EterniaEmu.Core.Config;

public class LogConfig
{
    public string LogPath { get; set; } = "logs";

    public string LogFile { get; set; } = "eternia_emu_.txt";

    public LogLevelType LogLevel { get; set; } = LogLevelType.Debug;

    public bool LogToFile { get; set; } = true;

    public bool LogToConsole { get; set; } = true;
}
