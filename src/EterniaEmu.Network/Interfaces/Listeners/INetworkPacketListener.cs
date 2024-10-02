using EterniaEmu.Network.Interfaces.Packets;

namespace EterniaEmu.Network.Interfaces.Listeners;

public interface INetworkPacketListener<in TPacket> where TPacket : INetworkPacket
{
    Task OnPacketReceivedAsync(string sessionId, TPacket packet);
}
