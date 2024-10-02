using System.Text;
using EterniaEmu.Network.Interfaces;
using EterniaEmu.Network.Interfaces.Packets;

namespace EterniaEmu.Network.Implementation;

public class AbstractNetworkPacket : INetworkPacket
{
    public int Size { get; } = 0;

    private int _position = 0;

    public virtual void Read(Span<byte> buffer)
    {
    }

    public virtual byte[] Write() => [];


    protected void ReadVoid(Span<byte> buffer, int size)
    {
        _position += size;
    }

    protected byte ReadByte(Span<byte> buffer)
    {
        var value = buffer[_position];
        _position += sizeof(byte);
        return value;
    }

    protected int ReadWord(Span<byte> buffer)
    {
        var value = BitConverter.ToInt16(buffer.Slice(_position, sizeof(short)));
        _position += sizeof(short);
        return value;
    }

    protected string ReadString(Span<byte> buffer, int size)
    {
        var value = Encoding.ASCII.GetString(buffer.Slice(_position, size).ToArray());
        _position += size;
        return value;
    }

    protected int ReadDword(Span<byte> buffer)
    {
        var value = BitConverter.ToInt32(buffer.Slice(_position, sizeof(int)));
        _position += sizeof(int);
        return value;
    }
}
