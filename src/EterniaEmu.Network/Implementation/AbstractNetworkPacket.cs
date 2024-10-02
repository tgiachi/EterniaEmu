using EterniaEmu.Network.Interfaces;

namespace EterniaEmu.Network.Implementation;


public class AbstractNetworkPacket : INetworkPacket
{
    public int Size { get; }
    public virtual void Read(Span<byte> buffer)
    {

    }

    public virtual byte[] Write() => [];
}
