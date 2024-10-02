using NetCoreServer;
using Serilog;
using Serilog.Core;

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

        _logger.Information("Received opCode: 0x{OpCode} from sessionId: {}", opCode.ToString("X2"), Id);

        var packet = EterniaTcpServer.CreatePacket(opCode);

        if (packet == null)
        {
            _logger.Warning("Unknown packet type: {OpCode}", opCode);
            return;
        }


        _logger.Information("Found packet type: {PacketType}", packet.GetType().Name);

        packet.Read(buffer.Skip(1).Take(packet.Size).ToArray());




        base.OnReceived(buffer, offset, size);
    }
}
