using EterniaEmu.Network.Interfaces.Packets;

namespace EterniaEmu.Network.Interfaces.Listeners;

public interface INetworkPacketListener
{
    Task<List<INetworkPacket>> OnPacketReceivedAsync(string sessionId, INetworkPacket packet);
}
