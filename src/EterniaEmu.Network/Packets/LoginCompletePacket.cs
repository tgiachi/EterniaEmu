using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.LoginComplete)]
public class LoginCompletePacket : AbstractNetworkPacket
{
    public override int Size => 0;

    public override int OpCode => (int)PacketTypeEnum.LoginComplete;
}
