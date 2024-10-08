namespace EterniaEmu.Network.Consts;

public enum PacketTypeEnum : int
{
    CharacterCreation = 0x00,
    Logout = 0x01,
    RequestMove = 0x02,
    Speech = 0x03,
    GodModeToggle = 0x04,
    AttackLastAttack = 0x05,
    RequestObjUse = 0x06,
    LoginSeed = 0xEF,
    LoginComplete = 0x55,

    LoginRequest = 0x80,
    LoginDenied = 0x82,
    GameServerList = 0xA8,
    SelectServer = 0xA0
}
