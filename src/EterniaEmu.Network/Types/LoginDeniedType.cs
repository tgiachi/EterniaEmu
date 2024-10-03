namespace EterniaEmu.Network.Types;

public enum LoginDeniedType : byte
{
    IncorrectPassword = 0x00,
    SomeoneIsLoggedIn = 0x01,
    AccountBlocked = 0x02,
    CredentialInvalid = 0x03,
    CommunicationProblem = 0x04,
    IgrConcurrencyLimit = 0x05,
    IgrTimeLimit = 0x06,
    IgrAuthFailed = 0x07,
}
