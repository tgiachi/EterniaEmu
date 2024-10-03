using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;
using EterniaEmu.Network.Types;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.LoginDenied)]
public class LoginDeniedPacket : AbstractNetworkPacket
{
    public override int Size => 1;

    public override int OpCode => (int)PacketTypeEnum.LoginDenied;

    public LoginDeniedType Reason { get; set; }

    public override byte[] Write() => new byte[] { (byte)Reason };
}
