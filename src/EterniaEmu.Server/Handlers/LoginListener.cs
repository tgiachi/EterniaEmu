using EterniaEmu.Core.Data;
using EterniaEmu.Network.Implementation.Listeners;
using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Interfaces.Packets;
using EterniaEmu.Network.Packets;
using EterniaEmu.Network.Types;

namespace EterniaEmu.Server.Handlers;

public class LoginListener : AbstractNetworkPacketListener<LoginRequestPacket>
{
    protected override async Task<List<INetworkPacket>> OnPacketReceivedAsync(
        EterniaTcpSession session, LoginRequestPacket packet
    )
    {
        var gameList = new GameServerListPacket();
        gameList.Servers.Add(
            new GameServerEntryData()
            {
                ServerName = "EterniaEmu",
                ServerIP = "127.0.0.1"
            }
        );
        return
        [
            gameList
        ];
    }
}
