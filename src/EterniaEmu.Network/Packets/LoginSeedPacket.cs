using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Packets;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.LoginSeed)]
public class LoginSeedPacket : AbstractNetworkPacket
{
    public override int Size => 21;

    public byte Cmd { get; set; }

    public int Seed { get; set; }

    public int MajorVersion { get; set; }

    public int MinorVersion { get; set; }

    public int Revision { get; set; }

    public int Prototype { get; set; }

    public override void Read(Span<byte> buffer)
    {
        Cmd = ReadByte(buffer);
        Seed = ReadDword(buffer);
        MajorVersion = ReadDword(buffer);
        MinorVersion = ReadDword(buffer);
        Revision = ReadDword(buffer);
        Prototype = ReadDword(buffer);
    }
}
