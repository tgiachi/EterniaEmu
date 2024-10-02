using System.Net;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Interfaces.Packets;
using NetCoreServer;
using Serilog;

namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpServer : TcpServer
{
    private readonly ILogger _logger = Log.ForContext<EterniaTcpServer>();

    private readonly Dictionary<byte, Type> _packetTypes = new();





    public EterniaTcpServer(IPAddress address, int port) : base(address, port)
    {
    }

    protected override TcpSession CreateSession() => new EterniaTcpSession(this);


    public void AddPacketType(byte opCode, Type packetType)
    {
        _packetTypes.Add(opCode, packetType);
    }

    public void AddPacketType(PacketTypeEnum opCode, Type packetType)
    {
        AddPacketType((byte)opCode, packetType);
    }

    public INetworkPacket? CreatePacket(byte opCode)
    {
        if (!_packetTypes.TryGetValue(opCode, out var packetType))
        {
            _logger.Warning("Unknown packet type: {OpCode}", opCode);
            return null;
        }

        return (INetworkPacket)Activator.CreateInstance(packetType);
    }
}
