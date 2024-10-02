using EterniaEmu.Network.Interfaces.Listeners;
using EterniaEmu.Network.Interfaces.Packets;
using NetCoreServer;

namespace EterniaEmu.Network.Interfaces.Server;

public interface IEterniaEmuTcpServer
{
    void Start();

    Task DispatchPacketAsync(Guid sessionId, INetworkPacket packet);
    void AddPacketListener<T>(INetworkPacketListener listener) where T : INetworkPacket;
}
