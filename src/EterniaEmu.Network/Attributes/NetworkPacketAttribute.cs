namespace EterniaEmu.Network.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NetworkPacketAttribute : Attribute
{
    public int PacketId { get; }

    public NetworkPacketAttribute(int packetId)
    {
        PacketId = packetId;
    }
}