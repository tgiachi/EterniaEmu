namespace EterniaEmu.Network.Interfaces.Server;

public interface IEterniaEmuTcpServer
{

    Task StartAsync();

    Task StopAsync();
}
