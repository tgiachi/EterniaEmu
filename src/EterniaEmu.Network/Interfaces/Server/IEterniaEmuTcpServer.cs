using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Listeners;
using EterniaEmu.Network.Interfaces.Packets;
using NetCoreServer;

namespace EterniaEmu.Network.Interfaces.Server;

public interface IEterniaEmuTcpServer
{
    void Start();

    Task DispatchPacketAsync(EterniaTcpSession session, INetworkPacket packet);
    void AddPacketListener<T>(INetworkPacketListener listener) where T : INetworkPacket;
}
