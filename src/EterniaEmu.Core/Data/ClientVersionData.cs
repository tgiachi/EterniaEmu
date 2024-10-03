namespace EterniaEmu.Core.Data;

public record ClientVersionData(int MajorVersion, int MinorVersion, int Revision, int Prototype)
{
    public override string ToString() => $"{MajorVersion}.{MinorVersion}.{Revision}.{Prototype}";
}
