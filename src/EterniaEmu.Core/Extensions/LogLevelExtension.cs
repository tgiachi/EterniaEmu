using EterniaEmu.Core.Types;
using Serilog.Events;

namespace EterniaEmu.Core.Extensions;

public static class LogLevelExtension
{
    public static LogEventLevel ToSerilogLevel(this LogLevelType level)
    {
        return level switch
        {
            LogLevelType.Trace => LogEventLevel.Verbose,
            LogLevelType.Debug => LogEventLevel.Debug,
            LogLevelType.Info  => LogEventLevel.Information,
            LogLevelType.Warn  => LogEventLevel.Warning,
            LogLevelType.Error => LogEventLevel.Error,
            LogLevelType.Fatal => LogEventLevel.Fatal,
            _                  => LogEventLevel.Information
        };
    }
}
