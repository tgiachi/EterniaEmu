using System.Text;
using EterniaEmu.Network.Interfaces.Packets;
using EterniaEmu.Network.Utils;

namespace EterniaEmu.Network.Implementation.Packets;

public class AbstractNetworkPacket : INetworkPacket
{
    public virtual int OpCode { get; } = -1;
    public virtual int Size { get; } = 0;

    public virtual void Read(Span<byte> buffer)
    {
        var reader = new PacketReader(buffer.ToArray(), buffer.Length, true);
        OnDecode(reader);

        reader = null;
    }

    public virtual byte[] Write() => [];


    protected virtual void OnDecode(PacketReader reader)
    {
    }
}
