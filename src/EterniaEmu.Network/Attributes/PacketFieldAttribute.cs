namespace EterniaEmu.Network.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class PacketFieldAttribute : Attribute
{
    public int Size { get; }

    public PacketFieldAttribute(int size)
    {
        Size = size;
    }
}
