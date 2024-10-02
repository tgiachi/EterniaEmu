namespace EterniaEmu.Network.Interfaces;

public interface INetworkPacket
{
    int Size { get; }

    void Read(Span<byte> buffer);

    byte[] Write();
}
