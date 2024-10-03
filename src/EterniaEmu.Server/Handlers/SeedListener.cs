using EterniaEmu.Core.Data;
using EterniaEmu.Network.Implementation.Listeners;
using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Packets;
using EterniaEmu.Network.Packets;

namespace EterniaEmu.Server.Handlers;

public class SeedListener : AbstractNetworkPacketListener<LoginSeedPacket>
{
    protected override Task<List<INetworkPacket>> OnPacketReceivedAsync(EterniaTcpSession session, LoginSeedPacket packet)
    {
        session.Seed = packet.Seed;

        session.ClientVersionData = new ClientVersionData(
            packet.MajorVersion,
            packet.MinorVersion,
            packet.Revision,
            packet.Prototype
        );
        return Task.FromResult(new List<INetworkPacket>());
    }
}
