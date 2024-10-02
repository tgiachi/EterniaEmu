namespace EterniaEmu.Core.Config;

public class ServerConfig
{
    public string Host { get; set; } = "0.0.0.0";

    public int Port { get; set; } = 2593;

    public bool SavePacketLogs { get; set; } = false;
}
