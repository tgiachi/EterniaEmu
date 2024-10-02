using System.Net;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Interfaces.Packets;
using EterniaEmu.Network.Interfaces.Server;
using NetCoreServer;
using Serilog;

namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpServer : TcpServer
{
    private readonly ILogger _logger = Log.ForContext<EterniaTcpServer>();

    private readonly Dictionary<byte, Type> _packetTypes = new();

    private readonly IEterniaEmuTcpServer _eterniaEmuTcpServer;

    public EterniaTcpServer(IPAddress address, int port, IEterniaEmuTcpServer eterniaEmuTcpServer) : base(address, port)
    {
        _eterniaEmuTcpServer = eterniaEmuTcpServer;
    }

    protected override TcpSession CreateSession() => new EterniaTcpSession(this, _eterniaEmuTcpServer);


    public void SendPackets(Guid sessionId, params INetworkPacket[] packets)
    {
        var session = FindSession(sessionId);

        if (session == null)
        {
            _logger.Warning("Session not found: {SessionId}", sessionId);
            return;
        }

        ((EterniaTcpSession)session).SendPackets(packets);
    }

    public void SendPacket(Guid sessionId, INetworkPacket packet)
    {
        var session = FindSession(sessionId);

        if (session == null)
        {
            _logger.Warning("Session not found: {SessionId}", sessionId);
            return;
        }

        ((EterniaTcpSession)session).SendPacket(packet);
    }


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
