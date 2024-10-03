using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;
using EterniaEmu.Network.Utils;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.SelectServer)]
public class SelectServerPacket : AbstractNetworkPacket
{
    public override int OpCode => (int)PacketTypeEnum.SelectServer;
    public override int Size => 3;

    public int ServerId { get; set; }

    protected override void OnDecode(PacketReader reader)
    {
        ServerId = reader.ReadInt16();
        base.OnDecode(reader);
    }
}
