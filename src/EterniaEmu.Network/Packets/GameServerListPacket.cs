using System.Net;
using EterniaEmu.Core.Data;
using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;
using EterniaEmu.Network.Utils;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.GameServerList)]
public class GameServerListPacket : AbstractNetworkPacket
{
    public override int Size => 0;

    public override int OpCode => (int)PacketTypeEnum.GameServerList;


    public List<GameServerEntryData> Servers { get; set; } = new();

    protected override void OnEncode(PacketWriter writer)
    {
        writer.Write((short)(40 * Servers.Count));
        writer.Write((byte)0x5D);
        writer.Write((short)Servers.Count);

        var index = 0;
        foreach (var server in Servers)
        {
            writer.Write((short)index);
            writer.WriteAsciiFixed(server.ServerName, 32);
            writer.Write((byte)0);
            writer.Write((byte)0);

            var addressIp = IPAddress.Parse(server.ServerIP).GetAddressBytes();
            Array.Reverse(addressIp);

            writer.Write(addressIp, 0, addressIp.Length);

            index++;
        }
    }
}
