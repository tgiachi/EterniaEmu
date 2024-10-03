using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Packets;

namespace EterniaEmu.Network.Interfaces.Listeners;

public interface INetworkPacketListener
{
    Task<List<INetworkPacket>> OnPacketReceivedAsync(EterniaTcpSession session, INetworkPacket packet);
}
