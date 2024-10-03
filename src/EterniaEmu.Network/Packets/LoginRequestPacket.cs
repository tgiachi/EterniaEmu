using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;
using EterniaEmu.Network.Utils;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.LoginRequest)]
public class LoginRequestPacket : AbstractNetworkPacket
{
    public override int Size => 62;

    public override int OpCode => (int)PacketTypeEnum.LoginRequest;


    public string UserName { get; set; }

    public string Password { get; set; }


    protected override void OnDecode(PacketReader reader)
    {
        UserName = reader.ReadStringSafe(30);
        Password = reader.ReadStringSafe(30);
        base.OnDecode(reader);
    }
}
