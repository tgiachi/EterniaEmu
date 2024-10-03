using System.Text;

namespace EterniaEmu.Network.Utils;

public class PacketReader
{
    private readonly byte[] m_Data;
    private readonly int m_Size;
    private int m_Index;

    public PacketReader(byte[] data, int size, bool fixedSize)
    {
        m_Data = data;
        m_Size = size;
        m_Index = fixedSize ? 1 : 3;
    }

    public byte[] Buffer => m_Data;
    public int Size => m_Size;

    public int Seek(int offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                m_Index = offset;
                break;
            case SeekOrigin.Current:
                m_Index += offset;
                break;
            case SeekOrigin.End:
                m_Index = m_Size - offset;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
        }

        return m_Index;
    }

    public int ReadInt32()
    {
        if ((m_Index + 4) > m_Size)
        {
            return 0;
        }

        int result = (m_Data[m_Index] << 24)
                     | (m_Data[m_Index + 1] << 16)
                     | (m_Data[m_Index + 2] << 8)
                     | m_Data[m_Index + 3];
        m_Index += 4;
        return result;
    }

    public short ReadInt16()
    {
        if ((m_Index + 2) > m_Size)
        {
            return 0;
        }

        short result = (short)((m_Data[m_Index] << 8) | m_Data[m_Index + 1]);
        m_Index += 2;
        return result;
    }

    public byte ReadByte() => (m_Index < m_Size) ? m_Data[m_Index++] : (byte)0;

    public uint ReadUInt32()
    {
        if ((m_Index + 4) > m_Size)
        {
            return 0;
        }

        uint result = (uint)((m_Data[m_Index] << 24)
                             | (m_Data[m_Index + 1] << 16)
                             | (m_Data[m_Index + 2] << 8)
                             | m_Data[m_Index + 3]);
        m_Index += 4;
        return result;
    }

    public ushort ReadUInt16()
    {
        if ((m_Index + 2) > m_Size)
        {
            return 0;
        }

        ushort result = (ushort)((m_Data[m_Index] << 8) | m_Data[m_Index + 1]);
        m_Index += 2;
        return result;
    }

    public sbyte ReadSByte() => (m_Index < m_Size) ? (sbyte)m_Data[m_Index++] : (sbyte)0;

    public bool ReadBoolean() => (m_Index < m_Size) && (m_Data[m_Index++] != 0);

    public string ReadUnicodeStringLE()
    {
        int start = m_Index;
        while ((m_Index + 1) < m_Size && (m_Data[m_Index] | (m_Data[m_Index + 1] << 8)) != 0)
        {
            m_Index += 2;
        }

        string result = Encoding.Unicode.GetString(m_Data, start, m_Index - start);
        m_Index += 2; // Skip null terminator
        return result;
    }

    public string ReadUnicodeStringLESafe(int fixedLength)
    {
        int bound = m_Index + (fixedLength << 1);
        if (bound > m_Size)
        {
            bound = m_Size;
        }

        int start = m_Index;
        while ((m_Index + 1) < bound && (m_Data[m_Index] | (m_Data[m_Index + 1] << 8)) != 0)
        {
            m_Index += 2;
        }

        string result = Encoding.Unicode.GetString(m_Data, start, m_Index - start);
        m_Index = bound; // Move to the end of the fixed length
        return result;
    }

    public string ReadUTF8StringSafe(int fixedLength)
    {
        if (m_Index >= m_Size)
        {
            m_Index += fixedLength;
            return string.Empty;
        }

        int bound = m_Index + fixedLength;
        if (bound > m_Size)
        {
            bound = m_Size;
        }

        int start = m_Index;
        while (m_Index < bound && m_Data[m_Index] != 0)
        {
            m_Index++;
        }

        string result = Encoding.UTF8.GetString(m_Data, start, m_Index - start);
        m_Index = bound; // Move to the end of the fixed length
        return result;
    }

    public string ReadUTF8String()
    {
        if (m_Index >= m_Size)
        {
            return string.Empty;
        }

        int start = m_Index;
        while (m_Index < m_Size && m_Data[m_Index] != 0)
        {
            m_Index++;
        }

        string result = Encoding.UTF8.GetString(m_Data, start, m_Index - start);
        m_Index++; // Skip null terminator
        return result;
    }

    public string ReadString()
    {
        int start = m_Index;
        while (m_Index < m_Size && m_Data[m_Index] != 0)
        {
            m_Index++;
        }

        string result = Encoding.ASCII.GetString(m_Data, start, m_Index - start);
        m_Index++; // Skip null terminator
        return result;
    }

    public string ReadStringSafe(int fixedLength)
    {
        int bound = m_Index + fixedLength;
        if (bound > m_Size)
        {
            bound = m_Size;
        }

        int start = m_Index;
        while (m_Index < bound && m_Data[m_Index] != 0)
        {
            m_Index++;
        }

        string result = Encoding.ASCII.GetString(m_Data, start, m_Index - start);
        m_Index = bound; // Move to the end of the fixed length
        return result;
    }
}
