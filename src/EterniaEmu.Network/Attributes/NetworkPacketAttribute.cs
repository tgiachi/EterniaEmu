using EterniaEmu.Network.Consts;

namespace EterniaEmu.Network.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NetworkPacketAttribute : Attribute
{
    public int PacketId { get; }

    public NetworkPacketAttribute(PacketTypeEnum packetId) => PacketId =  (int)packetId;
}
