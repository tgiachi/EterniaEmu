namespace EterniaEmu.Network.Data;

public class NetworkPacketDefinition
{
    public int PacketId { get; set; }

    public Type PacketType { get; set; }

    public int Size { get; set; }
}
