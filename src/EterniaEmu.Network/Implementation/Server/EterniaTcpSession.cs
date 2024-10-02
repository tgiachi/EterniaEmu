using EterniaEmu.Network.Interfaces.Packets;
using NetCoreServer;
using Serilog;


namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpSession : TcpSession
{
    private readonly ILogger _logger = Log.ForContext<EterniaTcpSession>();
    protected EterniaTcpServer EterniaTcpServer => (EterniaTcpServer)Server;

    public EterniaTcpSession(EterniaTcpServer server) : base(server)
    {
    }

    public void SendPackets(params INetworkPacket[] packets)
    {
        foreach (var packet in packets)
        {
            SendPacket(packet);
        }
    }

    public void SendPacket(INetworkPacket packet)
    {
        var buffer = new byte[packet.Size + 1];

        if (packet.OpCode == -1)
        {
            _logger.Warning("[{Id}] OpCode is not set for packet: {PacketType}", Id, packet.GetType().Name);
            return;
        }

        buffer[0] = (byte)packet.OpCode;


        packet.Write().CopyTo(buffer, 1);

        SendAsync(buffer);
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var opCode = buffer[0];

        _logger.Debug("[{Id}] Received opCode: 0x{OpCode}", Id, opCode.ToString("X2"));

        var packet = EterniaTcpServer.CreatePacket(opCode);

        if (packet == null)
        {
            _logger.Warning("[{Id}] Unknown packet type: {OpCode}", Id, opCode);
            return;
        }


        _logger.Debug("[{Id}] Found packet type: {PacketType}", Id, packet.GetType().Name);

        packet.Read(buffer.Skip(1).Take(packet.Size).ToArray());


        base.OnReceived(buffer, offset, size);
    }
}
