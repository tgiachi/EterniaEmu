namespace EterniaEmu.Network.Consts;

public enum PacketTypeEnum : byte
{
    CharacterCreation = 0x00,
    Logout = 0x01,
    RequestMove = 0x02,
    Speech = 0x03,
    GodModeToggle = 0x04,
    AttackLastAttack = 0x05,
    RequestObjUse = 0x06,

}
