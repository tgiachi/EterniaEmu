using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Listeners;
using EterniaEmu.Network.Interfaces.Packets;

namespace EterniaEmu.Network.Implementation.Listeners;

public class AbstractNetworkPacketListener<TPacket> : INetworkPacketListener
    where TPacket : INetworkPacket
{
    public Task<List<INetworkPacket>> OnPacketReceivedAsync(EterniaTcpSession session, INetworkPacket packet)
    {
        if (packet is TPacket typedPacket)
        {
            return OnPacketReceivedAsync(session, typedPacket);
        }


        throw new InvalidCastException($"Expected packet of type {typeof(TPacket).Name}, but got {packet.GetType().Name}");
    }

    protected virtual Task<List<INetworkPacket>> OnPacketReceivedAsync(EterniaTcpSession session, TPacket packet)
    {
        return Task.FromResult(new List<INetworkPacket>());
    }
}
