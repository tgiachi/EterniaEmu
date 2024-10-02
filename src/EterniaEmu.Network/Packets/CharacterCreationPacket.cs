using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation;
using EterniaEmu.Network.Types;

namespace EterniaEmu.Network.Packets;

[NetworkPacket(PacketTypeEnum.CharacterCreation)]
public class CharacterCreationPacket : AbstractNetworkPacket
{
    public int Unused1 { get; set; }

    public int Unused2 { get; set; }

    public byte Unused3 { get; set; }

    public string Name { get; set; }

    public GenderAndRaceType GenderAndRace { get; set; }

    public int Strength { get; set; }

    public int Dexterity { get; set; }

    public int Intelligence { get; set; }

    public int Skill1 { get; set; }

    public int Skill1Amount { get; set; }

    public int Skill2 { get; set; }

    public int Skill2Amount { get; set; }

    public int Skill3 { get; set; }

    public int Skill3Amount { get; set; }

    //word
    public int SkinColor { get; set; }

    public int HairStyle { get; set; }

    public int HairColor { get; set; }

    public int BeardStyle { get; set; }

    public int BeardColor { get; set; }

    public int StartingCity { get; set; }

    public byte Unused4 { get; set; }

    public int Slot { get; set; }

    // dword ip address

    public int IpAddress { get; set; }

    public int ShirtColor { get; set; }

    public int PantsColor { get; set; }

}
