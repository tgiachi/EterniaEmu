using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;
using EterniaEmu.Network.Utils;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.LoginSeed)]
public class LoginSeedPacket : AbstractNetworkPacket
{
    public override int Size => 21;

    public int Seed { get; set; }

    public int MajorVersion { get; set; }

    public int MinorVersion { get; set; }

    public int Revision { get; set; }

    public int Prototype { get; set; }


    protected override void OnDecode(PacketReader reader)
    {
        Seed = reader.ReadInt32();
        MajorVersion = reader.ReadInt32();
        MinorVersion = reader.ReadInt32();
        Revision = reader.ReadInt32();
        Prototype = reader.ReadInt32();
    }
}
