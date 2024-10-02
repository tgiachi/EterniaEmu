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


    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var opCode = buffer[0];

        _logger.Information("[{Id}] Received opCode: 0x{OpCode}", Id, opCode.ToString("X2"));

        var packet = EterniaTcpServer.CreatePacket(opCode);

        if (packet == null)
        {
            _logger.Warning("[{Id}] Unknown packet type: {OpCode}", Id, opCode);
            return;
        }


        _logger.Information("[{Id}] Found packet type: {PacketType}", Id, packet.GetType().Name);

        packet.Read(buffer.Skip(1).Take(packet.Size).ToArray());


        base.OnReceived(buffer, offset, size);
    }
}
