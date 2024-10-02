namespace EterniaEmu.Network.Interfaces.Packets;

public interface INetworkPacket
{
    int OpCode { get; }
    int Size { get; }

    void Read(Span<byte> buffer);

    byte[] Write();
}
