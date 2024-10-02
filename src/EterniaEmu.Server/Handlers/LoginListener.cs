using EterniaEmu.Network.Interfaces.Listeners;
using EterniaEmu.Network.Interfaces.Packets;
using EterniaEmu.Network.Packets;

namespace EterniaEmu.Server.Handlers;

public class LoginListener : INetworkPacketListener
{
    public async Task<List<INetworkPacket>> OnPacketReceivedAsync(string sessionId, INetworkPacket packet)
    {
        return new List<INetworkPacket>() { new LoginCompletePacket() };
    }
}
